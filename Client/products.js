const pageSize = 8;

let config;
loadConfig().then(async (conf) => {
    config = conf;
    addPaginationButtonsEventListener();
    await getProductsFromApi(1);
});

const addToCartEventListner = () => {
    const addToCartForms = document.querySelectorAll('#add-to-cart-form');
    addToCartForms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            const formData = new FormData(form);
            const productId = formData.get('productId');

            const baseApi = config.baseApiUrl;
            const absoluteApi = "shoppingcart/cartitem";

            const token = localStorage.getItem('token');
            const headers = {
                'Authorization': `Bearer ${token}`
            };

            try {
                await axios.post(`${baseApi}/${absoluteApi}`, {
                    productId: productId,
                    quantity: 1
                }, { headers });

                showToast('Product added to cart!');
            } catch (error) {
                console.error('Error adding item to cart:', error);
                showToast('Error adding product to cart');
            }
        });
    });
}

const addPaginationButtonsEventListener = () => {
    const prevButton = document.querySelector('.previous');
    const nextButton = document.querySelector('.next');
    const pageNumber = document.querySelector('.page-number');

    prevButton.addEventListener('click', async () => {
        await getProductsFromApi(Number(pageNumber.textContent) - 1);
    });

    nextButton.addEventListener('click', async () => {
        await getProductsFromApi(Number(pageNumber.textContent) + 1);
    });
}

const showPorducts = (products) => {
    if (products.length != 0) {
        const productsGrid = document.querySelector('.products-grid');
        productsGrid.innerHTML = '';
    }

    products.forEach(product => {
        const productCard = document.createElement('div');
        productCard.classList.add('product-card');
        productCard.innerHTML = `
            <div class="product-card">
                <div class="image-container">
                    <img class="image" src="${product.imageUrl}" alt="${product.name}">
                </div>
                <div>
                    <h4 class="item-name">${product.name}</h4>
                    <p class="item-price">$${product.price}</p>
                </div>
                <form id="add-to-cart-form">
                        <input type="hidden" name="productId" value="${product.id}">
                        <button type="submit" class="add-to-cart"><i class="fa-solid fa-cart-shopping"></i> Add to Cart</button>
                </form>
            </div>
        `;

        document.querySelector('.products-grid').appendChild(productCard);
    });
}

const getProductsFromApi = async (pageNumber, sort, search) => {
    const baseApi = config.baseApiUrl;
    const absoluteApi = "product";

    const response = await axios.get(`${baseApi}/${absoluteApi}?pagenumber=${pageNumber}&pagesize=${pageSize}`);
    const productsRest = response.data;
    const products = productsRest.data;


    if (products.length != 0) {
        showPorducts(products);
        const pageNumberElement = document.querySelector('.page-number');
        pageNumberElement.textContent = pageNumber;
    }

    addToCartEventListner();
}

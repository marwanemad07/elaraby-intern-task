let config;
loadConfig().then(async (conf) => {
    config = conf;
    const cartResponse = await getCartItemsFromApi();
    const cartData = cartResponse.data;
    
    renderCartItems(cartData);
    document.getElementById('checkoutBtn').addEventListener('click', checkOut);
});

const getCartItemsFromApi = async () => {
    const baseApi = config.baseApiUrl;
    const absoluteApi = "shoppingcart";

    const headers = getTokenHeader();

    try {
        const response = await axios.get(`${baseApi}/${absoluteApi}`, { headers });
        const responseRest = response.data;
        return responseRest;
    } catch (error) {
        console.error('Error getting cart items:', error);
    }
}

const renderCartItems = (cartData) => {
    let cartItemsContainer = document.getElementById('cartItems');

    cartItemsContainer.innerHTML = '';

    cartData.cartItems.forEach(item => {
        cartItemsContainer.innerHTML += `
            <div class="cart-item" data-id="${item.id}">
                <img src="${item.imageUrl}" alt="${item.productName}">
                <div class="item-details">
                    <p class="item-name">${item.productName}</p>
                    <p class="item-price">$${item.price}</p>
                    <div class="item-quantity">
                        <button class="quantity-control" onclick="changeQuantity('${item.id}', ${item.productId}, -1)">-</button>
                        <span class="quantity">${item.quantity}</span>
                        <button class="quantity-control" onclick="changeQuantity('${item.id}', ${item.productId}, 1)">+</button>
                    </div>
                </div>
                <button class="remove-item" onclick="removeItem('${item.id}')"><i class="fa-solid fa-trash""></i></button>
            </div>
        `;
    });

    updateOrderSummary(cartData);
}

const changeQuantity = async (itemId, productId, change) => {
    const itemElement = document.querySelector(`.cart-item[data-id="${itemId}"]`);

    let quantity = parseInt(itemElement.querySelector('.quantity').innerText);

    quantity += change;
    if (quantity < 1) {
        quantity = 1;
        return;
    }

    itemElement.querySelector('.quantity').innerText = quantity;

    await updateItemQuantityOnServer(productId, change);
}

const updateOrderSummary = (cartData) => {
    let totalItems = 0;
    let totalPrice = cartData.totalPrice;

    cartData.cartItems.forEach(item => {
        totalItems += item.quantity;
    });

    document.getElementById('totalItems').innerText = totalItems;
    document.getElementById('totalPrice').innerText = totalPrice.toFixed(2);
}

const updateItemQuantityOnServer = async (productId, quantity) => {
    const baseApi = config.baseApiUrl;
    const absoluteApi = "shoppingcart/cartitem";

    const headers = getTokenHeader();

    try {
        const response = await axios.post(`${baseApi}/${absoluteApi}`, { productId, quantity }, { headers });
        const responseRest = response.data;
        const cartData = responseRest.data;

        updateOrderSummary(cartData);
    } catch (error) {
        console.error('Error updating item quantity:', error);
    }
}

const removeItem = async (itemId) => {
    const baseApi = config.baseApiUrl;
    const absoluteApi = "shoppingcart/cartitem";

    const headers = getTokenHeader();

    try {
        await axios.delete(`${baseApi}/${absoluteApi}/${itemId}`, { headers });
    } catch (error) {
        console.error('Error removing item:', error);
    }

    const cartResponse = await getCartItemsFromApi();
    const cartData = cartResponse.data;

    renderCartItems(cartData);
}

const checkOut = async () => {
    const baseApi = config.baseApiUrl;
    const absoluteApi = "Order";

    const headers = getTokenHeader();

    try{
        await axios.post(`${baseApi}/${absoluteApi}`, {}, { headers });
        showToast("Order placed successfully!");
        
    } catch (error) {
        console.error('Error checking out:', error);
        showToast('Error placing order');
    }

    renderCartItems({ cartItems: [], totalPrice: 0 });
};

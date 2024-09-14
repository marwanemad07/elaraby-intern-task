let config;
loadConfig().then(async (c) => {
    config = c;

    const isAdded = await addCategoriesOptions();

    if (isAdded) {
        document.getElementById('add-product-form')
            .addEventListener('submit', formHandler);
    }
})


const formHandler = async (e) => {
    e.preventDefault();

    const baseApi = config.baseApiUrl;
    const absoluteApi = "product";

    hideErrors();

    let name = document.getElementById('name').value.trim();
    let price = parseFloat(document.getElementById('price').value);
    let quantity = parseInt(document.getElementById('quantity').value);
    let description = document.getElementById('description').value.trim();
    let image = document.getElementById('image').files[0];
    let categoryId = parseInt(document.getElementById('category').value);

    let isValid = true;
    if (name === '' && isValid) {
        showError('Name is required.', 'nameError');
        isValid = false;
    }

    if (isNaN(price) || price <= 0 && isValid) {
        showError('Price must be a positive number.', 'priceError');
        isValid = false;
    }

    if (isNaN(quantity) || quantity <= 0 && isValid) {
        showError('Quantity must be a positive integer.', 'quantityError');
        isValid = false;
    }

    if (description === '' && isValid) {
        showError('Description is required.', 'descriptionError');
        isValid = false;
    }

    if (!image && isValid) {
        showError('Image is required.', 'imageError');
        isValid = false;
    }

    if (isNaN(categoryId) && isValid) {
        showError('Category is required.', 'categoryError');
        isValid = false;
    }

    if (isValid) {
        const token = localStorage.getItem('token');
        const headers = {
            'Authorization': token
        }

        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Price', price);
        formData.append('Quantity', quantity);
        formData.append('Description', description);
        formData.append('ImageFile', image);
        formData.append('CategoryId', categoryId);

        await sendRequest(`${baseApi}/${absoluteApi}`, formData, headers);
    }
}

const sendRequest = async (url, data) => {
    try {
        const headers = getTokenHeader();

        const response = await axios.post(url, data, {
            headers: headers
        });

        const responseRest = response.data;
        if (responseRest.statusCode = 201) {
            showToast('Product added successfully.');
            document.getElementById('add-product-form').reset();
        } else {
            showToast('Failed to add product.');
            showError(responseRest.message, 'apiError');
        }
    } catch (e) {
        console.error("Error adding product:", e);
        showToast('Something went wrong!');
    }
}

const addCategoriesOptions = async () => {
    const baseApi = config.baseApiUrl;
    const absoluteApi = "category";

    document.getElementById('category').innerHTML = '';
    try {
        const response = await axios.get(`${baseApi}/${absoluteApi}`);
        const responseRest = response.data;
        const categories = responseRest.data;

        categories.forEach(category => {
            let option = document.createElement('option');
            option.value = category.id;
            option.textContent = category.name;
            document.getElementById('category').appendChild(option);
        });
    } catch (e) {
        console.error("Error loading categories:", e);
        return false;
    }
    return true;
}

const showError = (message, elementId) => {
    document.getElementById(elementId).textContent = message;
    document.getElementById(elementId).style.display = 'block';
}

const hideErrors = () => {
    document.getElementById('nameError').style.display = 'none';
    document.getElementById('priceError').style.display = 'none';
    document.getElementById('quantityError').style.display = 'none';
    document.getElementById('descriptionError').style.display = 'none';
    document.getElementById('imageError').style.display = 'none';
    document.getElementById('categoryError').style.display = 'none';
    document.getElementById('apiError').style.display = 'none';
}
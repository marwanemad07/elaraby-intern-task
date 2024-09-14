let config;
loadConfig().then((c) => {  
    config = c;

    document.getElementById('signupForm')
            .addEventListener('submit', formHandler);
});

const formHandler = async (e) => {
    e.preventDefault();

    const baseApi = config.baseApiUrl;
    const absoluteApi = "account/register";

    hideErrors();

    let firstName = document.getElementById('firstName').value.trim();
    let lastName = document.getElementById('lastName').value.trim();
    let username = document.getElementById('username').value.trim();
    let street = document.getElementById('street').value.trim();
    let city = document.getElementById('city').value.trim();
    let zipCode = document.getElementById('zipCode').value.trim();
    let email = document.getElementById('email').value.trim();
    let password = document.getElementById('password').value;
    let confirmPassword = document.getElementById('confirmPassword').value;

    const data = {
        firstName,
        lastName,
        username,
        street,
        city,
        zipCode,
        email,
        password,
        confirmPassword
    }

    const isValid = checkErrors(data);
    
    if(isValid){
        try{
            const formData = {
                firstName: firstName,
                lastName: lastName,
                username: username,
                street: street,
                city: city,
                zipCode: zipCode,
                email: email,
                password: password
            }

            const response = await axios.post(`${baseApi}/${absoluteApi}`, formData);

            const responseRest = response.data;
            if(responseRest.statusCode == 200) {
                showToast('User created successfully!');
                window.location.href = 'login.html';
            } 
            else {
                showToast('Error creating user');
            }
        } catch(e) {
            console.log("Signup error", e);
            const response = e.response;
            const responseRest = response.data;

            if(responseRest.statusCode == 400) {
                const errors = responseRest.details;
                errors.forEach((error) => {
                    console.log('Error: ' + error);
                    addErrorToErrorList(error);
                });
            }
            showToast('Something went wrong!');
        }
    }
}

const checkErrors = (data) => {
    let isValid = true;
    if (data.firstName === '' && isValid) {
        showError('First name is required.', 'firstNameError');
        isValid = false;
    }

    if (data.lastName === '' && isValid) {
        showError('Last name is required.', 'lastNameError');
        isValid = false;
    }

    if (data.username === '' && isValid) {
        showError('Username is required.', 'usernameError');
        isValid = false;
    }

    if (data.email === '' && isValid) {
        showError('Email is required.', 'emailError');
        isValid = false;
    }

    var emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailRegex.test(data.email) && isValid) {
        console.log('Email: ' + data.email);
        showError('Email is invalid.', 'emailError');
        isValid = false;
    }

    if (data.street === '' && isValid) {
        showError('Street is required.', 'streetError');
        isValid = false;
    }

    if (data.city === '' && isValid) {
        showError('City is required.', 'cityError');
        isValid = false;
    }

    if(zipCode === '' && isValid) {
        showError('Zip code is required.', 'zipCodeError');
        isValid = false;
    }

    var zipCodeRegex = /^\d{5}$/;
    if (!zipCodeRegex.test(data.zipCode) && isValid) {
        console.log('Zip code: ' + data.zipCode);
        showError('Zip code should be 5 digits.', 'zipCodeError');
        isValid = false;
    }

    if (data.password === '' && isValid) {
        showError('Password is required.', 'passwordError');
        isValid = false;
    }

    if (data.confirmPassword === '' && isValid) {
        showError('Confirm password is required.', 'confirmPasswordError');
        isValid = false;
    }

    if (data.password !== data.confirmPassword && isValid) {
        showError('Passwords do not match.', 'confirmPasswordError');
        isValid = false;
    }

    return isValid;
}

const showError = (message, elementId) => {
    document.getElementById(elementId).textContent = message;
    document.getElementById(elementId).style.display = 'block';
}

const addErrorToErrorList = (error) => {
    const li = document.createElement('li');
    li.innerHTML = `<span class="error-message" style="display: block;">${error}</span>`;
    document.getElementById('errorList').appendChild(li);
}

const hideErrors = () => {
    document.getElementById('firstNameError').style.display = 'none';
    document.getElementById('lastNameError').style.display = 'none';
    document.getElementById('usernameError').style.display = 'none';
    document.getElementById('streetError').style.display = 'none';
    document.getElementById('cityError').style.display = 'none';
    document.getElementById('zipCodeError').style.display = 'none';
    document.getElementById('emailError').style.display = 'none';
    document.getElementById('passwordError').style.display = 'none';
    document.getElementById('confirmPasswordError').style.display = 'none';

    const errorList = document.querySelectorAll('.error-list li');
    errorList.forEach((errorItem) => {
        errorItem.style.display = 'none';
    });
}
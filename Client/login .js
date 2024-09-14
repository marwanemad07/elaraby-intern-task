let config;
loadConfig().then(async (c) => {
    config = c;
    document.getElementById('loginForm').addEventListener('submit', formHandler);
});

const formHandler = async (e) => {
    e.preventDefault();

    const baseApi = config.baseApiUrl;
    const absoluteApi = "account/login";
    
    document.getElementById('emailError').style.display = 'none';
    document.getElementById('passwordError').style.display = 'none';

    let email = document.getElementById('email').value.trim();
    let password = document.getElementById('password').value.trim();
    let isValid = true;

    var emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;

    if (!emailRegex.test(email)) {
        document.getElementById('emailError').textContent = 'Email is invalid.';
        document.getElementById('emailError').style.display = 'block';
        isValid = false;
    }

    if (password === '' && isValid) {
        document.getElementById('passwordError').textContent = 'Password is required.';
        document.getElementById('passwordError').style.display = 'block';
        isValid = false;
    }

    if (isValid) {
        const loginData = {
            email: email,
            password: password
        };
        
        try{
            const response = await axios.post(`${baseApi}/${absoluteApi}`, loginData);
            
            const responseRest = response.data;
            const token = responseRest.data.token;
            localStorage.setItem('token', token);
            window.location.href = 'products.html';
            console.log(response);

        } catch (error) {
            console.error('Login failed:', error);
            alert('Login failed. Please check your credentials.');
        }
    }
}
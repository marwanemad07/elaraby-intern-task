window.onload = () => {
    loadNavbar();

    const token = localStorage.getItem('token');

    if (token) {
        try {
            const decode = jwt_decode(token);

            const currentTime = Math.floor(Date.now() / 1000);
            if (currentTime > decode.exp) {
                logout();
            } else {
                const roles = decode.role;
                const rolesArray = Array.isArray(roles) ? roles : [roles];
                showAuthenticatedNav(rolesArray);
            }
        } catch (error) {
            console.error('Error decoding token:', error);
            showLoginNav();
        }
    } else {
        showLoginNav();
    }
}

const loadNavbar = () => {
    const nav = document.getElementById("navbar");

    nav.innerHTML = `
    <nav>
        <div class="nav-logo">
            <ul>
                <li><a href="products.html">Online Shopping Logo</a></li>
            </ul>
        </div>
        <div class="nav-links">
            <ul id="nav-links">
                <li><a href="products.html">Products</a></li>
            </ul>
        </div>
        <div class="nav-user">
            <ul id="auth-links">
                
            </ul>
        </div>
    </nav>
    `;
}

const showLoginNav = () => {
    const ul = document.getElementById('auth-links');
    ul.innerHTML = `
        <li>
            <a href="login.html"><i class="fa-solid fa-sign-in"></i> Login</a>
        </li>
    `
}

const showAuthenticatedNav = (roles) => {
    const ul = document.getElementById('auth-links');
    ul.innerHTML = '';
    console.log(roles);

    if (roles === undefined || roles.some(role => role.toLowerCase() === 'user')) {
        ul.innerHTML += `
        <li>
            <a href="cart.html"><i class="fa-solid fa-cart-shopping"></i> Cart</a>
        </li>
        `;
    }

    ul.innerHTML += `
    <li>
        <a href="login.html" class="logout"><i class="fa-solid fa-sign-out"></i> Logout</a>
    </li>
    `;
    if (roles != roles.some(role => role.toLowerCase() === 'admin')) {
        const navLinks = document.getElementById('nav-links');
        const li = document.createElement('li');
        li.innerHTML = `<a href="add-product.html">New Product</a>`;
        navLinks.appendChild(li);
    }

    const logoutButton = document.querySelector(".logout");
    logoutButton.addEventListener('click', logout);
}


const logout = () => {
    localStorage.removeItem('token');
    showLoginNav();
    window.location.href = "login.html";
}
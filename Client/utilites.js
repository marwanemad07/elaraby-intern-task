const loadConfig = async () => {
    try {
        const response = await fetch('./config.json');
        return await response.json();
    } catch (error) {
        console.error('Error loading config:', error);
        return null;
    }
}

const showToast = (message) => {
    const toast = document.getElementById('toast');
    toast.textContent = message;
    toast.classList.add('show');

    setTimeout(() => {
        toast.classList.remove('show');
    }, 3000);
}

const getTokenHeader = () => {
    const token = localStorage.getItem('token');
    const headers = {
        'Authorization': `Bearer ${token}`
    };
    return headers
}
const domainArderss = window.location.origin;
console.log(domainArderss);

function showLoginModal() {
    const loginModal = new bootstrap.Modal('#login-modal');
    loginModal.show();
}

async function login() {
    const loginUrl = '/user/login';
    const form = document.getElementById('login-form');
    form.addEventListener('submit', function (e) {
        e.preventDefault();
    });

    let email = document.getElementById('email-input').value;
    let pswd = document.getElementById('password-input').value;
    if (email && pswd) {
        let data = {
            email: email,
            password: pswd
        };

        let result = await fetch(domainArderss + loginUrl, {
            body: JSON.stringify(data),
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
        });
        if (result.ok) {
            window.location.replace(domainArderss);
        }
    }
    else {
        //some vallidation here
    }

}

function showLogoutModal() {
    const logoutModal = new bootstrap.Modal('#logout-modal');    
    logoutModal.show();
}

async function logout() {
    const testUrl = '/user/logout';
    let logoutResult = await fetch(domainArderss + testUrl,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
        });

    if (logoutResult.ok) {
        window.location.replace(domainArderss);
    }
}
function login(username, password) {

    fetch(`https://localhost:7202/api/Account/Login`, {
        method: 'POST',
        body: JSON.stringify({
            username: username,
            password: password
        }),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(data => {

        window.location.href = `index.html?user=${data.username}`;
    })
    .catch(error => {
        console.error('Błąd logowania:', error);
    });
}

function register(username, password, confirmPassword) {

    if (password !== confirmPassword) {
        console.error('Hasła nie są identyczne');
        return;
    }


    fetch(`https://localhost:7202/api/Account/Register`, {
        method: 'POST',
        body: JSON.stringify({
            username: username,
            password: password
        }),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(data => {

        window.location.href = 'login.html';
    })
    .catch(error => {
        console.error('Błąd rejestracji:', error);
    });
}



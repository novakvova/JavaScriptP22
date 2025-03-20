const formRegister = document.getElementById('formLogin');

formRegister.onsubmit = (e) => {
    show_loading();
    ClearErrors();
    e.preventDefault();
    const xhr = new XMLHttpRequest();
    const url = "https://goose.itstep.click/api/Account/login";

    const data = {
        email: document.getElementById("email").value,
        password: document.getElementById("password").value
    };

    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const resp = xhr.responseText;
                const token = JSON.parse(resp).token;

                localStorage.setItem("token", token);
                localStorage.setItem('isLoggedIn', true);
                //ChangeMenu();
                location.href = '/user/profile.html';
                hide_loading();
            } else {
                HandleError(xhr.responseText);
                hide_loading();
            }
        }
    };

    xhr.send(JSON.stringify(data));
}

function HandleError(responseText) {
    const response = JSON.parse(responseText);
    if (response.errors) {
        for (const field in response.errors) {
            if (response.errors.hasOwnProperty(field)) {
                const fieldErrors = response.errors[field];
                console.log("field:", field, "error", fieldErrors);

                errorMessages = fieldErrors;
                switch (field) {
                    case "email": { emailError.hidden = false; emailError.textContent = errorMessages; break; }
                    case "password": { passwordError.hidden = false; passwordError.textContent = errorMessages; break; }
                    case "invalid": { passwordError.hidden = false; passwordError.textContent = errorMessages; break; }
                }
            }
        }
    }
}

function ClearErrors() {
    emailError.hidden = true;
    passwordError.hidden = true;
}
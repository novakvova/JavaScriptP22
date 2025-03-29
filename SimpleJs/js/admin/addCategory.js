
const categoryForm = document.getElementById('categoryForm');
const categoryPB = document.getElementById('categoryPB');
const categoryName = document.getElementById('name');

categoryForm.onsubmit = (e) => {
    ClearErrors();
    e.preventDefault();
    const xhr = new XMLHttpRequest();
    const url = "https://goose.itstep.click/api/Categories/add";

    const data = {
        title: document.getElementById("name").value,
        priority: document.getElementById("priority").value,
        image: document.getElementById("avatar").src,
        urlSlug: document.getElementById("slug").value
    };

    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const resp = xhr.responseText;
                pbContainer.hidden = false;
                let promise = new Promise(function (resolve, reject) {
                    let i = 0;
                    let interval = setInterval(() => {

                        if (i <= 100) {
                            categoryPB.style.width = i + "%";
                            i += 5;
                        } else {
                            clearInterval(interval);
                            location.href = "/pages/admin/adminPanel.html";
                            resolve();
                        }
                    }, 500);
                });

            } else {
                HandleError(xhr.responseText);
            }
        }
    };

    xhr.send(JSON.stringify(data));
}

function HandleError(responseText) {

    const response = JSON.parse(responseText);
    if (response.error) {
        slugError.hidden = false;
        slugError.textContent = response.error;
    }
    else if (response.invalid) {
        imageError.hidden = false;
        imageError.textContent = response.invalid;
    }
}

function ClearErrors() {
    slugError.hidden = true;
    imageError.hidden = true;
}
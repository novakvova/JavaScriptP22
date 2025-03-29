
const categoryForm = document.getElementById('categoryForm');
const categoryPB = document.getElementById('categoryPB');
const categoryName = document.getElementById('name');
const category = localStorage.getItem('categoryEdit');

categoryForm.onsubmit = (e) => {
    ClearErrors();
    e.preventDefault();
    let newImage = null;

    const avatarSrc = document.getElementById('avatar').src;
    if (avatarSrc.startsWith("data:")) {
        newImage = avatarSrc;
    }

    const xhr = new XMLHttpRequest();

    const data = {
        id: new URLSearchParams(window.location.search).get('id') || null,
        title: document.getElementById("name").value,
        priority: document.getElementById("priority").value,
        ...(newImage !== null && { image: newImage }), // Додаємо поле image тільки якщо newImage !== null
        urlSlug: document.getElementById("slug").value
    };
    const url = `https://goose.itstep.click/api/Categories/edit?${data}`;


    xhr.open("PUT", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");

    let promise = new Promise(function (resolve, reject) {
        pbContainer.hidden = false;
        let i = 0;
        let interval = setInterval(() => {

            if (i <= 100) {
                categoryPB.style.width = i + "%";
                i += 5;
            } else {
                clearInterval(interval);
                resolve();
            }
        }, 500);

    })
        ;

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const resp = xhr.responseText;
                location.href = "/pages/admin/adminPanel.html";


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

show_loading();

window.addEventListener('load', async () => {

    const id = new URLSearchParams(window.location.search).get('id') || null;
    console.log("id", id);

    const response = await axios.get(`https://goose.itstep.click/api/Categories/get/${id}`, {
        headers: {
            'Accept': '*/*'
        }
    });
    const { data } = response;

    document.getElementById('avatar').src = `https://goose.itstep.click/images/100_${data.image}`;
    document.getElementById('name').value = data.title;
    document.getElementById('priority').value = data.priority;
    document.getElementById('slug').value = data.urlSlug;

    hide_loading()
});
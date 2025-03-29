
const categoryForm = document.getElementById('categoryForm');
const categoryPB = document.getElementById('categoryPB');
const categoryName = document.getElementById('name');
const category = localStorage.getItem('categoryEdit');

categoryForm.onsubmit = (e) => {
    ClearErrors();
    e.preventDefault();
    let newImage;

    if (document.getElementById('avatar').src == `https://goose.itstep.click/images/100_${JSON.parse(category).image}`) {

        var img = new Image();
        img.src = document.getElementById('avatar').src;
        img.setAttribute('crossOrigin', 'anonymous');
        img.onload = function () {
            var canvas = document.createElement("canvas");
            canvas.width = document.getElementById('avatar').width;
            canvas.height = document.getElementById('avatar').height;
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0);
            var dataURL = canvas.toDataURL("image/png");

            newImage = dataURL.replace(/^data:image\/?[A-z]*;base64,/, "");
        }


    } else {
        newImage = document.getElementById('avatar').src;
    }

    const xhr = new XMLHttpRequest();

    const data = {
        id: JSON.parse(category).id,
        title: document.getElementById("name").value,
        priority: document.getElementById("priority").value,
        image: newImage,
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
                localStorage.removeItem('categoryEdit');
                location.href = "/pages/admin/categories/categories.html";


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
});
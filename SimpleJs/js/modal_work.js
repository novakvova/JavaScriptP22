let cropper;
let uploadImageURL;
var file_blob;
var image = document.getElementById('image');
let avatar;

var uploadImage = document.getElementById('uploadImage');

const leftRotate = document.getElementById('leftRotate');
const rightRotate = document.getElementById('rightRotate');

const formRegister = document.getElementById("formRegister");

formRegister.onsubmit = (e) => {
    e.preventDefault(); //відмінити станартну повіденку форми
    const formData = {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        password: document.getElementById("password").value,
        avatar: document.getElementById("avatar").src
    }
    const oldItems = JSON.parse(localStorage.users ?? "[]");
    console.log("Old list", oldItems);

    let items = [...oldItems, formData];

    let json = JSON.stringify(items);
    localStorage.setItem("users", json);
    //localStorage.users = json;
    console.log("Submit form", json);
    location.href = "/users.html"; //переходимо на іншу сторінку сайту
}


leftRotate.onclick = function (e) {
    if (cropper) {
        cropper.rotate(90);
    }
}
rightRotate.onclick = function (e) {
    if (cropper) {
        cropper.rotate(-90);
    }
}

//js - вміє сам по id шукати елеенти - автоматично
chooseBT.onclick = function (e) {

    avatar = document.getElementById('avatar');

    console.log("Chose image", avatar.src);
    image.src = avatar.src;
    if (cropper)
        cropper.destroy();

    cropper = new Cropper(image, {
        aspectRatio: 1,
        viewMode: 1
    });
}

uploadImage.onchange = (event) => {

    const { target } = event;
    const { files } = target;

    const file = files[0];
    if (file) {
        if (/^image\/\w+/.test(file.type)) {

            if (uploadImageURL) {
                URL.revokeObjectURL(uploadImageURL);
            }

            image.src = uploadImageURL = URL.createObjectURL(file);
            
            if (cropper) {
                cropper = cropper.destroy();
            }
            cropper = new Cropper(image, {
                aspectRatio: 1,
                viewMode: 1
            });
            uploadImage.value = "";
        }
        else {
            //window.alert('Please choose an image file.');
        }
    }
}

saveImage.onclick = function (e) {
    if (cropper) {

        var base64 = cropper.getCroppedCanvas().toDataURL();

        avatar.src = uploadImageURL = base64;
        //cropper.getCroppedCanvas().toBlob(function (blob) {
        //    file_blob = new File([blob], "some_random_name.jpg");

        //    if (uploadImageURL) {
        //        URL.revokeObjectURL(uploadImageURL);
        //    }
        //    avatar.src = uploadImageURL = URL.createObjectURL(file_blob);
        
        //});
    }
}

cancel.onclick = function (e) {
    //image.src = avatar.src;
    //if (cropper) {
    //    cropper = cropper.destroy();
    //}
    //image.src = avatar.src;
   
    //    cropper = cropper.destroy();
    
    //cropper = new Cropper(image, {
    //    aspectRatio: 1,
    //    viewMode: 1
    //});
}
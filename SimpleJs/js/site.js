const loadDOM = (path) => {
    let xhr = new XMLHttpRequest();
    xhr.open("GET", path, false); 
    xhr.send();
    document.write(xhr.response);
}

//window.API_BASE_URL = 'https://goose.itstep.click';
window.API_BASE_URL = 'http://localhost:5227';
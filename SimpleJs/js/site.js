const loadDOM = (path) => {
    let xhr = new XMLHttpRequest();
    xhr.open("GET", path, false); 
    xhr.send();
    document.write(xhr.response);
}
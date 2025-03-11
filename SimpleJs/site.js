(function () {
    "use strict";
    function getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    let listRandom = [];

    for (let i = 0; i < 10; i++) {
        listRandom.push(getRandomInt(18, 60));
    }

    listRandom.forEach(x => {
        console.log("Foreach items ", x);
    });

    let list = [2, 4, 5];
    list.push(7);
    list[4] = "Світлана Миколаївна"; //Вставити примусово, дуже жорстко
    list[8] = -1; //Щось цікаве?



    for (let i = 0; i < list.length; i++) {
        console.log("Hello my friends", list[i]);
    }

    //list.map(x => {
    //    console.log("map", x);
    //});

    let age = '25';

    if (age === 25) {
        console.log("Хороший вік. Можуть мобілізувати :)");
    }

    //let man = confirm("Ви чоловік?");

    //console.log("male = ", man);

    //let name = prompt("Як Вас звати?");

    //alert(`Привіт ${name}!`);


    //alert("Це я Вова :)");

    let t = 34; //number, string, boolean, bigint, undefined, null,object, symbol
    var b = true;
    //t = 33;
    console.log("Привіт! Класна погода :)", t);
    if (1) {
        var robert = "Robert";
    }
    //console.log(robert);
    function helloMyFriends() {
        var victor = "Віктор Підкаблучник";
        console.log("Наш баратан", victor);

        if (victor) {
            let malvina = "Мальвіна Підбалучна";
        }
        console.log(robert);
    }
    helloMyFriends();

    //console.log(victor);

})();

var userNav = document.getElementById('nav-user');

function ChangeMenu() {

    let user = null;
    if (localStorage.getItem("token")) {
        user = jwt_decode(localStorage.getItem("token"));
    }

    if (typeof user !== "undefined" && user) {

        if (user.roles == "user") {
            userNav.innerHTML = `
    <button id="accountButton" type="button" class="flex text-lg bg-gray-800 rounded-full md:me-0 focus:ring-4 focus:ring-gray-300 " id="user-menu-button" aria-expanded="false" data-dropdown-toggle="user-dropdown" data-dropdown-placement="bottom">
    <a href="/pages/user/profile.html">
            <img id="accountImage" class="account-image rounded-full" src="https://goose.itstep.click/images/${user.image}" alt="user photo">

    </a>
    </button>
          `;

        }
        else if (user.roles == "admin") {
            userNav.innerHTML = `
    <button id="accountButton" type="button" class="flex text-lg bg-gray-800 rounded-full md:me-0 focus:ring-4 focus:ring-gray-300 " id="user-menu-button" aria-expanded="false" data-dropdown-toggle="user-dropdown" data-dropdown-placement="bottom">
    <a href="/pages/admin/adminPanel.html">
            <img id="accountImage" class="account-image rounded-full" src="/images/adminIcon.png" alt="user photo">

    </a>
    </button>
          `;
        }
    }
    else {
        userNav.innerHTML = `
<a href="/login.html" style="margin-right:5px" class="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 text-lg">Login</a>
            <a href="/" style="margin-left:5px" class="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 text-lg">Register</a>
                    `;
    }

}

ChangeMenu();
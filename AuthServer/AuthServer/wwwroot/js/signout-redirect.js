window.addEventListener("load", function () {
    var a = document.querySelector("a.PostLogoutRedirectUri");
    if (a) {
        console.log(a.href);
        window.location = a.href;
    }
});

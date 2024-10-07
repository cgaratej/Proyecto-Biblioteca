// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("formEdit").addEventListener("change", function (event) {
    var isChecked = document.getElementById("isAgreed").checked;
    var element = document.getElementById("changePassword");
    var element2 = document.getElementById("changePassword2");

    if (isChecked) {
        element.className = "form-group";
        element2.className = "form-group";
    } else {
        element.className = "d-none";
        element2.className = "d-none";
    }
});

/*document.getElementById("formEdit").addEventListener("change", function (event) {
    var isChecked = document.getElementById("isAgreed").checked;
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;

    if (isChecked && password != password2) {
        event.preventDefault();
        document.getElementById("btnSubmit").className = "disable";
    }
});*/
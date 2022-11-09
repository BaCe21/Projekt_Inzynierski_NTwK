// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
function openErrorModal(strMessage) {
    var myDiv = document.getElementById("MyErrorBody");
    myDiv.innerHTML = strMessage;
    $('#myError').modal('show');
}

function openSuccessModal(strMessage) {
    var myDiv = document.getElementById("MySuccessBody");
    myDiv.innerHTML = strMessage;
    $('#mySuccess').modal('show');
}
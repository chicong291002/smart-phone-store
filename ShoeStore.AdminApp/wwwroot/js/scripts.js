/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
    // 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});


function chenDauChamNganGiua() {
    var inputElement = document.getElementById("myNumberInput");
    var inputValue = inputElement.value;

    var outputValue = inputValue.replace(/0{4,}/g, function (match) {
        var dotCount = Math.floor(match.length / 2);
        if (match.length % 2 === 0) {
            return ".".repeat(dotCount) + ".";
        } else {
            return ".".repeat(dotCount);
        }
    });

    inputElement.value = outputValue;
}
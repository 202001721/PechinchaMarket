﻿document.addEventListener('DOMContentLoaded', function () {
    var elements = document.querySelectorAll('.authentication-input-text-div');
    elements.forEach(function (element) {
        var height = element.getBoundingClientRect().height;
        element.style.height = (height - 24) + 'px';
    });
});

function updateUrlParameter(key, value) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set(key, value);
    const newUrl = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + urlParams.toString();
    window.history.replaceState(null, null, newUrl);
}

function updateFileName(input) {
    var fileContainer = input.parentNode; // Get the parent node of the input (the .insert-file container)
    var fileNameElement = fileContainer.querySelector('.file-name');
    fileNameElement.innerText = input.files[0] ? input.files[0].name : 'No file chosen';
}

/**
 * This function toggles the input-text type between 'password' and 'text', allowing the password to be displayed to the user.
 * (This function assumes that the button to hide or to show the password is a sibling of the input-text)
 * 
 * @param element correspondes to the button that hiddes or makes the password vissiable 
 */
function togglePassword(element) {
    var passwordContainer = element.parentNode;
    var password = passwordContainer.querySelector('input[type="text"], input[type="password"]');

    if (password.type == "password") {
        password.type = "text";
        element.classList.remove("password-hidden");
        element.classList.add("password-visible");
    } else if (password.type == "text") {
        password.type = "password";
        element.classList.add("password-hidden");
        element.classList.remove("password-visible");
    }
}

/**
 * Function to adjust the position of the show-password input button based on the text-danger child height
 * 
 * @param element correspondes to the parent div with the text-danger and show-passowrd button ('.authentication-input-text-div')
 */
function adjustTogglePassword(element) {
    var passwordInput = element.querySelector('.password-hidden, .password-visible');
    var dangerChild = element.querySelector('.text-danger');
    if (passwordInput && dangerChild) {
        var initialHeight = dangerChild.getBoundingClientRect().height;
        var currentBottom = parseInt(getComputedStyle(passwordInput).bottom);

        var observer = new MutationObserver(function (mutationsList) {
            mutationsList.forEach(function (mutation) {
                var currentHeight = dangerChild.getBoundingClientRect().height;
                if (currentHeight !== initialHeight) {
                    var dangerChildHeight = currentHeight;
                    passwordInput.style.bottom = (currentBottom + dangerChildHeight) + 'px';
                    initialHeight = currentHeight;
                }
            });
        });

        observer.observe(dangerChild, { attributes: true });
    }
}

function observeTextDanger() {
    var elements = document.querySelectorAll('.authentication-input-text-div');
    elements.forEach(function (element) {
        adjustTogglePassword(element);
    });
}

document.addEventListener('DOMContentLoaded', observeTextDanger);

function goBack() {
    window.history.back();
}
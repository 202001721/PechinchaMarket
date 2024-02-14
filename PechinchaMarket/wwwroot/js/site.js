﻿/**
 *  This event is so all authentication inputs text heigth is adjusted ignoring a size of 24 px;
 */
document.addEventListener('DOMContentLoaded', function () {
    var elements = document.querySelectorAll('.authentication-input-text-div');
    elements.forEach(function (element) {
        var height = element.getBoundingClientRect().height;
        element.style.height = (height - 24) + 'px';
    });
});

/**
 * This function will update the current url with a prefix key and value.
 * 
 * @param {any} key the key name in the url
 * @param {any} value the value of that key in the url
 */
function updateUrlParameter(key, value) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set(key, value);
    const newUrl = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + urlParams.toString();
    window.history.replaceState(null, null, newUrl);
}

/**
 * This function will update a label that corresponds to the uploaded file name on the basis that its a uncle of the input type file
 * 
 * @param input the input type file 
 */
function updateFileName(input) {
    var fileContainer = input.parentNode.parentNode; // Get the parent node of the input (the .insert-file container)
    var fileNameElement = fileContainer.querySelector('.file-name');
    fileNameElement.innerText = input.files[0] ? input.files[0].name : 'Nenhum ficheiro escolhido';
}

/**
 * This function toggles the input-text type between 'password' and 'text', allowing the password to be displayed to the user.
 * (This function assumes that the button to hide or to show the password is a sibling of the input-text)
 * 
 * @param element the button that hiddes or makes the password vissiable 
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
    var inputElement = document.querySelector('input[type="text"]');
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

    if (inputElement && dangerChild) {
        var initialHeight = dangerChild.getBoundingClientRect().height;
        var inputHeight = 40;
        var observer = new MutationObserver(function (mutationsList) {
            mutationsList.forEach(function (mutation) {
                var currentHeight = dangerChild.getBoundingClientRect().height;
                if (currentHeight !== initialHeight) {
                    var dangerChildHeight = currentHeight;
                    element.style.height = (inputHeight + dangerChildHeight) + 'px';
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

/**
 *  This function will change the current window to the previous opened on the history
 */
function goBack() {
    window.history.back();
}

function sugest(userInput, searchBar) {
    if (userInput !== '') {
        fetch(`/Search/GetSugestiveNames?input=${userInput}`)
            .then(response => response.json())
            .then(suggestions => {
                var suggestionslist = document.getElementById("search-sugestions");

                if (suggestions.length > 0 && suggestionslist) {
                    //clean sugestions
                    suggestionslist.innerHTML = '';
                    suggestionslist.style.display = 'block';

                    suggestions.forEach(suggestion => {
                        //bold
                        const difference = findDifference(suggestion, userInput);

                        var listItem = document.createElement('div');
                        listItem.onclick = function () {
                            var searchText = document.getElementById('searchInput');
                            if (searchText) {
                                searchText.value = userInput + difference; // Replace "Your desired text" with the text you want to insert
                                document.getElementById('searchForm').submit(); // Submit the form
                            }
                        }

                        var span1 = document.createElement('span');
                        var span2 = document.createElement('span');
                        span1.textContent = userInput;
                        span2.textContent = difference;

                        listItem.appendChild(span1);
                        listItem.appendChild(span2);
                        suggestionslist.appendChild(listItem);
                    });
                    if (searchBar) {
                        searchBar.style.borderBottom = "none";
                        searchBar.style.borderRadius = "15px 15px 0px 0px";
                        suggestionslist.style.width = suggestionslist.parentNode.offsetWidth + 'px';
                    }
                } else if (suggestionslist){
                    //clean sugestions
                    suggestionslist.innerHTML = '';
                    suggestionslist.style.display = 'none';
                    if (searchBar) {
                        searchBar.style.borderBottom = "";
                        searchBar.style.borderRadius = "";
                    }
                }
            });
    }
}

function findDifference(str1, str2) {
    var i = str1.length - str2.length;
    if (i > 0) {
        return str1.substring((str1.length - i));
    }
}


document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const phaseParam = urlParams.get('search');
    if (phaseParam !== null) {
        var navbars = document.querySelectorAll('.nav-search-div');
        navbars.forEach(navbar => {
            var textinput = navbar.querySelector('input[type="text"]');
            textinput.value = phaseParam;
        });
    } 
});
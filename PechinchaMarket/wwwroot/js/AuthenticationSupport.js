
function toggleSection(index, element) {
    // Hide all sections
    for (let i = 0; i < 3; i++) {
        document.getElementById('section' + i).classList.add('display-none');
    }

    // Remove 'selected' class from all li elements
    let menuItems = document.getElementById('menu').getElementsByTagName('li');
    for (let i = 0; i < menuItems.length; i++) {
        menuItems[i].classList.remove('selected-support');
    }

    // Show the selected section
    document.getElementById('section' + index).classList.remove('display-none');

    // Add 'selected' class to the clicked li element
    element.classList.add('selected-support');
}


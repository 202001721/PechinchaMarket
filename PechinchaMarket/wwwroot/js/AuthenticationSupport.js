document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const sectionParam = urlParams.get('section');
    if (sectionParam !== null) {
        toggleSection(parseInt(sectionParam), document.getElementById('menu').getElementsByTagName('li')[parseInt(sectionParam)]);
    } else {
        defaultSectionIndex = 0;
        toggleSection(defaultSectionIndex, document.getElementById('menu').getElementsByTagName('li')[defaultSectionIndex]);
    }
});

function toggleSection(index, element) {
    for (let i = 0; i < 3; i++) {
        document.getElementById('section' + i).classList.add('display-none');
    }

    let menuItems = document.getElementById('menu').getElementsByTagName('li');
    for (let i = 0; i < menuItems.length; i++) {
        menuItems[i].classList.remove('selected-support');
    }

    document.getElementById('section' + index).classList.remove('display-none');

    element.classList.add('selected-support');

    updateUrlParameter('section', index);
}
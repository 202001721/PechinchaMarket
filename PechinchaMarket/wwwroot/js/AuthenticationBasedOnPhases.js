document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const phaseParam = urlParams.get('phase');
    if (phaseParam !== null) {
        toogleSignUpFase(parseInt(phaseParam), 2);
    } else {
        defaultSectionIndex = 0;
        toogleSignUpFase(defaultSectionIndex, 2);
    }
});

function toogleSignUpFase(index, phaseslength) {
    for (let i = 0; i < phaseslength; i++) {
        document.getElementById('phase' + i).classList.add('display-none');
        var phasetext = document.getElementById('text-phase' + i);
        if (phasetext) {
            if (!phasetext.classList.contains('display-none')) {
                phasetext.classList.add('display-none');
            }            
        }
    }

    document.getElementById('phase' + index).classList.remove('display-none');
    var phasetexti = document.getElementById('text-phase' + index);
    if (phasetexti) {
        phasetexti.classList.remove('display-none');
    }


    updateUrlParameter('phase', index);
}
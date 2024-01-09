const radioButtons = document.querySelectorAll('input[type=radio]');

console.log('inJS');

radioButtons.forEach(radio => {
    radio.addEventListener('change', (e) => {

        radioButtons.forEach(otherbutton => {
            otherbutton.checked = false;
        });

        radio.checked = true;
        
    });
});
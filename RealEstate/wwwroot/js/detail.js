
const pictures = document.querySelectorAll('.sub-photos>img');

pictures.forEach(selectedPicture => {
    selectedPicture.addEventListener('click', () => {
        const mainPicture = document.querySelector('.detail-photo>img');
        mainPicture.src = selectedPicture.src;

        pictures.forEach(picture => {
            picture.classList.remove('selected-sub-photo');
        });

        selectedPicture.classList.add('selected-sub-photo');
    });
});

const viewAllSubPhotos = (viewAllLink) => {
    const photos = document.querySelectorAll('.sub-photos>img');

    viewAllLink.classList.remove('dark-layer-over');
    viewAllLink.querySelector('label').classList.add('d-none');

    photos.forEach(photo => {
        photo.classList.remove('d-none');
        console.log(photo.getAttribute('alt'));
    });

}

const pictures = document.querySelectorAll('.sub-photos img');
//const viewAllLink = document.querySelector('.view-all-link');
//pictures.push(viewAllLink.querySelector('img'));

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
    const photos = document.querySelectorAll('.sub-photos img');
    viewAllLink.classList.remove('dark-layer-over');
    viewAllLink.querySelector('label').classList.add('d-none');

    photos.forEach(photo => {
        photo.classList.remove('d-none');
    });
    e.stopPropagation();
}

const clearInquiryForm = () => {
    const children = document.querySelector('#inquiryForm>.interested-form').children;

    Array.from(children).forEach(attr => {
        attr.removeAttribute('disabled');
        attr.value = '';
    });
    document.querySelector('.interested-button').removeAttribute('disabled');
}

const chat = document.querySelector("#search-button");
chat.addEventListener("click", (event) => {
    const input = document.querySelector("#search-input");
    inputText = input.value.trim();
    if (inputText === '') {
        event.preventDefault();
    }
});

const filterButton = document.querySelector(".filter-button");

filterButton.addEventListener("click", (event) => {
    const filterMenu = document.querySelector(".filter-menu-container");

    filterMenu.classList.toggle("d-none");
});

var viewCount = 0;
document.addEventListener("DOMContentLoaded", () => {
    const viewMoreButton = document.querySelector(".items-view-all");

    viewMoreOnClick();
    viewMoreButton.addEventListener("click", viewMoreOnClick);
});

const viewMoreOnClick = () => {
    const offers = document.querySelector(".offers-list").children;
    viewCount += 6;
    changeVisibilityOffers(offers);
}

const changeVisibilityOffers = (offers) => {
    for (let i = 0; i < offers.length; i++) {
        offers[i].classList.remove("d-none");
    }

    for (let i = viewCount; i < offers.length; i++) {
        offers[i].classList.add("d-none");
    }

    if(viewCount > offers.length) {
        document.querySelector(".items-view-all").classList.add("d-none");
    }
}


const element = document.querySelector("#search-button");
element.addEventListener("click", (event) => {
    const input = document.querySelector("#search-input");
    inputText = input.value.trim();
    if (inputText === '') {
        event.preventDefault();
    }
});

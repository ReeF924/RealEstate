
const chat = document.querySelector("#search-button");
chat.addEventListener("click", (event) => {
    const input = document.querySelector("#search-input");
    inputText = input.value.trim();
    if (inputText === '') {
        event.preventDefault();
    }
});

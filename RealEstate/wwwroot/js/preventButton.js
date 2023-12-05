

const element = document.querySelector("#search-button");
console.log(element.innerHTML);
element.addEventListener("click", (event) => {
    const input = document.querySelector("#search-input");
    inputText = input.value.trim();
    if (inputText === '') {
        event.preventDefault();
    }
});
console.log("preventButton.js");
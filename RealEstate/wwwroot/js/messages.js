document.addEventListener("DOMContentLoaded", function () {
    const messageSendInput = document.querySelector(".message-send-input");
    messageSendInput.focus();
    console.log(messageSendInput);
}); 

const chat = document.querySelector(".chat-container");
chat.scrollTop = chat.scrollHeight;


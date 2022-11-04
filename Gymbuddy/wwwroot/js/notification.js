var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
connection.on("Notification", function (number) {
    console.log(number);
    var icon = document.getElementById("messNumber");
    icon.innerText = number.newMessagesNumber;
});
connection.on("Message", function (message) {
    console.log("on receive message");
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
});
connection.start().then().catch(function (err) {
    return console.error(err.toString());
})

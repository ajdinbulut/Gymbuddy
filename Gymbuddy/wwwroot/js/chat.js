var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
var send = document.getElementById("sendToUser");
connection.start().then(function () {
    send.addEventListener("click", function (event) {
        var message = document.getElementById("messageInput").value;
        let receiver = document.getElementById("receiver").value;
        $.ajax({
            type: 'POST',
            url: '@Url.Action("Message","Chat")',
            dataType: "json",
            data: {
                Message: message,
                Receiver: receiver
            },
            success: function (result) {

                connection.invoke("SendToUser", result.value, message).then(function () {
                    console.log(message);
                }).catch(function (err) {
                    return console.error(err.toString());
                });
                var li = document.createElement("li");
                li.textContent = message;
                document.getElementById("messagesList").appendChild(li);
            },
            error: function (req, status, error) {
                console.log(req);
            }
        })

        event.preventDefault();
    });
}).catch(function (err) {
    return console.error(err.toString());
})

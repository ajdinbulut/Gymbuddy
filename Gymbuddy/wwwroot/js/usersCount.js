    var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/connectedUsers").build();

    connectionUserCount.on("updateActiveUsers", function (value) {
        var newCountSpan = document.getElementById("usersConnected");
        newCountSpan.innerText = value.toString();


    })


    connectionUserCount.start().then(function () {
        connectionUserCount.invoke("GetConnectionId").then(function (id) {
            console.log(id);
            document.getElementById("connectionId").innerText = id;
            $.ajax({
                type: "POST",
                url: '@Url.Action("ConnectionId","Home")',
                data: { Id: id },
                dataType: "json",
                success: function (result) {
                    console.log(result);
                },
                error: function (req, error) {
                    console.log(error);
                }
            })
        })
    });

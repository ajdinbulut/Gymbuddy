var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/connectedUsers").build();

connectionUserCount.on("updateActiveUsers",function (value) {
    var newCountSpan = document.getElementById("usersConnected");
    newCountSpan.innerText = value.toString();
})

function fullfiled() {
    console.log("CONNECTED")
}
function rejeceted() {

}
connectionUserCount.start().then(fullfiled, rejeceted);
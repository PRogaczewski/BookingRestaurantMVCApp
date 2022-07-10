let screenInput = $("#hour");
let restIdInput = $("#restId");
let dateInput = $("#date");
let peopleInput = $("#seats");

document.getElementById("hour").onchange = function () { CheckForTable() };
document.getElementById("date").onchange = function () { ResetHour() };
document.getElementById("seats").onchange = function () { ResetHour() };

function ResetHour() {
    document.getElementById("hour").value = "None";
}
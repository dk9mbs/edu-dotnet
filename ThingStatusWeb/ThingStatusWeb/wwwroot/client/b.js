function sayHello() {
    var compiler = document.getElementById("compiler").value;
    var framework = document.getElementById("framework").value;
    return "Hello from " + compiler + " and " + framework + "!";
}
//debugger
try {
    var name1 = document.getElementById("txtName").value;
    alert(name1);
}
catch (e) {
    console.log(e);
    alert(e);
}
//# sourceMappingURL=b.js.map
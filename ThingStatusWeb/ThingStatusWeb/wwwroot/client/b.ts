function sayHello() {
    const compiler = (document.getElementById("compiler") as HTMLInputElement).value;
    const framework = (document.getElementById("framework") as HTMLInputElement).value;
    return `Hello from ${compiler} and ${framework}!`;
}

//debugger
try {
    var name1 = (document.getElementById("txtName") as HTMLInputElement).value;
    alert(name1);
} catch (e) {
    console.log(e);
    alert(e);
}

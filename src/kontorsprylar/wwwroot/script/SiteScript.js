function isvaliduser(e) {
    e.preventDefault();
    $.get("/login/testlogin", { 'Email': $("#Email").val(), 'Password': $("#Password").val() }, function (data) {
        alert(data);
        if (data) {
        $('#loginModal').modal('hide');
        }

    });


}

function isvaliduser(e) {
    e.preventDefault();
    $.get("/login/testlogin", { 'Email': $("#Email").val(), 'Password': $("#Password").val() }, function (data) {
        
        if (data != null) {
            alert("Du har nu loggat in med E-post " + data)
            $('#loginModal').modal('hide');
            $("#loginstate").html(" <a href=\"/login/logout\" class=\"btn btn-danger\">Logga ut</a>")
        }
    });
}
function userregistrate(e) {
    e.preventDefault();
    $.post("Registrate/index", $("#regform").serialize(), function (data) {
        alert(data);
        if (data) {
            $('#regmodal').modal('hide');
        }
    });
}

function isvaliduser(e) {
    e.preventDefault();
    $.get("/login/testadminlogin", { 'Email': $("#Email").val(), 'Password': $("#Password").val() }, function (data) {

        if (data != null) {
            alert("Du har nu loggat in med E-post " + data)
            $('#loginModal').modal('hide');
            $(location).attr('href', '/admin/adminpage')
        }
    });
}

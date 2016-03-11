function isvaliduser(e) {
    e.preventDefault();
    $.get("/login/testlogin", { 'Email': $("#Email").val(), 'Password': $("#Password").val() }, function (data) {
        
        if (data != null) {
            alert("Du har nu loggat in med E-post " + data)
            $('#loginModal').modal('hide');
            $("#loginstate").html(" <a href=\"/login/logout\" class=\"btn btn-danger\">Logga ut</a>");
        }
        else {
            $("summary").html("Felaktiga inloggningsuppgifter");
            alert("Nu gick något åt helvete, har du koll på dina förbannade inloggningsuppgifter...?? IDIOT!!");
            
        }
    });
}
function userregistrate(e) {
    e.preventDefault();
    $.post("Registrate/index", $("#regform").serialize(), function (data) {
        if (data) {
            $('#regmodal').modal('hide');
            $("#loginstate").html(" <a href=\"/login/logout\" class=\"btn btn-danger\">Logga ut</a>");
        }
    });
}

function addtocart(pid) {
    $.get("/ShoppingCart/AddToCart", { 'PID': pid }, function (data) {
        $("#shoppingcart").html(data)
    });
}

jQuery(document).on('click', '.mega-dropdown', function (e) {
    e.stopPropagation()
})
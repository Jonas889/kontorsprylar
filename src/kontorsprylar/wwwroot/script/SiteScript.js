function isvaliduser(e) {
    e.preventDefault();
    $.post("/login/testlogin", { 'Email': $("#Email").val(), 'Password': $("#Password").val() }, function (data) {
        
        if (data != null) {
            alert("Du har nu loggat in med E-post " + data)
            $('#loginModal').modal('hide');
            changebuttons();
        }
        else {
            //$("summary").html("Felaktiga inloggningsuppgifter");
            alert("Nu gick något åt helvete, har du koll på dina förbannade inloggningsuppgifter...?? IDIOT!!");
            
        }
    });
}
function userregistrate(e) {
    e.preventDefault();
    $.post("Registrate/index", $("#regform").serialize(), function (data) {
        if (data) {
            $('#regmodal').modal('hide');
            changebuttons();
        }
    });
}

function addtocart(pid) {
    $.get("/ShoppingCart/AddToCart", { 'PID': pid }, function (data) {
        $("#shoppingcart").html(data)
    });
}

function gotoreg() {
    $('#loginModal').modal('hide');
    $('#loginModal').on('hidden.bs.modal', function (e) {
        $.get("/registrate/index", null, function (data) {
            $("#modal").html(data);
            $.validator.unobtrusive.parse($("#modal"));
            $('#regmodal').modal('show');
        });
    });
}

jQuery(document).on('click', '.mega-dropdown', function (e) {
    e.stopPropagation()
})

function changebuttons() {
    $("#loginstate").html(" <a href=\"/login/logout\" class=\"btn btn-danger\"><span class=\"glyphicon glyphicon-log-out \"></span>&nbsp;Logga ut</a>");
    $("#mypages").html(" <button type=\"button\" class=\"btn btn-danger\"><span class=\"glyphicon glyphicon-user\"></span>&nbsp;Mina sidor</button>");
}
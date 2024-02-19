$(document).ready(function () {
     var redirect = $("#hidRedirectIndex").val().trim();
     if (redirect.length > 0)
          Redirecciona(redirect);
});

function Redirecciona(url)
{
     $(".alert").show();
     $(".alert").addClass('alert-danger');
     $(".alert").html("<strong><span class='lnr lnr-cross-cirlce fa fa-clock-o'></span>&nbsp; Cerrando Sesión...</strong>");
     $(window).scrollTop(0);
     setTimeout(function () {
          $(".alert").removeClass('alert-danger');
          $(".alert strong").remove();
          $(".alert").hide();
          window.location.href = url;
     }, 5000);
}
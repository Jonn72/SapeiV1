(function () {
    $(".card_horizontal").each(function (index, el) {
        $(this).on('click', function () {
            $(this).toggle("slow");
            var psToken = $(this).find(".card-stacked").find('#psToken').text();
            if (psToken.trim().length > 0) {
                $('#BodyPrincipal').load('../../Gnosis/AdministrarPublicacionesDepartamento', 'psTokenDepartamento=' + psToken);
            }
        });
    });

    $("#all_btn").on('click', function () {
        $('#BodyPrincipal').load('../../Gnosis/AdministrarPublicacionesDepartamento', 'psTokenDepartamento=All');
    });


    $(".card-image").find("img").each(function () {
        if (location.hostname != "sii.ittlahuac.edu.mx")
        {
            var url = $(this).attr("src");
            url=url.replace("sii.ittlahuac.edu.mx", "192.168.9.245");
            $(this).attr("src", url);
        }
        

    });

})();



    var MisionyVision = document.getElementById('MyVText');
    var Valores = document.getElementById('ValoresText');
    var Politicas = document.getElementById('PoliticasText');
    var General = document.getElementById('General');

function Muestra() {
    $("#MyVText").show();
    $("#ValoresText").hide();
    $("#PoliticasText").hide();
    $("#General").hide();
    //    MisionyVision.style.display = 'block';
    //Valores.style.display = 'none';
    //Politicas.style.display = 'none';
    //General.style.display = 'none'
    }

    function Muestra1() {
        $("#MyVText").hide();
        $("#ValoresText").show();
        $("#PoliticasText").hide();
        $("#General").hide();
    }

    function Muestra2() {
        $("#MyVText").hide();
        $("#ValoresText").hide();
        $("#PoliticasText").show();
        $("#General").hide();
    }

    function Muestra3() {
        $("#MyVText").hide();
        $("#ValoresText").hide();
        $("#PoliticasText").hide();
        $("#General").show();
    }

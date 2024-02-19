$("#txtLunesFin, #txtLunesIni").blur(function () {
     ValidaHorario($("#txtLunesIni"), $("#txtLunesFin"));
});
$("#txtMartesFin, #txtMartesIni").blur(function () {
     ValidaHorario($("#txtMartesIni"), $("#txtMartesFin"));
});
$("#txtMiercolesFin, #txtMiercolesIni").blur(function () {
     ValidaHorario($("#txtMiercolesIni"), $("#txtMiercolesFin"));
});
$("#txtJuevesFin, #txtJuevesIni").blur(function () {
     ValidaHorario($("#txtJuevesIni"), $("#txtJuevesFin"));
});
$("#txtViernesFin, #txtViernesIni").blur(function () {
     ValidaHorario($("#txtViernesIni"), $("#txtViernesFin"));
});
$("#txtSabadoFin, #txtSabadoIni").blur(function () {
     ValidaHorario($("#txtSabadoIni"), $("#txtSabadoFin"));
});

function ValidaHorario(hora_inicio, hora_fin) {
     var comienza = $(hora_inicio).val();
     var termina = $(hora_fin).val();
     var ini;
     var fin;
     if (comienza.length == 0 && termina.length == 0) {
          return true;
     }

     if (comienza.length == 0 || termina.length == 0) {
          $(hora_fin).val("");
          return false;
     }
     ini = parseInt(comienza.split(":")[0]);
     fin = parseInt($(hora_fin).val().split(":")[0]);
     if (fin <= ini) {
          $(hora_fin).val("");
          return false;
     }
     return true;
}

function ValidaTodosDias() {
     if (!ValidaHorario($("#txtLunesIni"), $("#txtLunesFin")))
          return false;
     if (!ValidaHorario($("#txtMartesIni"), $("#txtMartesFin")))
          return false;
     if (!ValidaHorario($("#txtMiercolesIni"), $("#txtMiercolesFin")))
          return false;
     if (!ValidaHorario($("#txtJuevesIni"), $("#txtJuevesFin")))
          return false;
     if (!ValidaHorario($("#txtViernesIni"), $("#txtViernesFin")))
          return false;
     if (!ValidaHorario($("#txtSabadoIni"), $("#txtSabadoFin")))
          return false;
     return true;
}

function RegresaHorario() {
     var horario = "";
     var inicio = $("#txtLunesIni").val();
     if (inicio.length > 0)
          horario += "LUN-" + inicio + "-" + $("#txtLunesFin").val() + "|";

     inicio = $("#txtMartesIni").val();
     if (inicio.length > 0)
          horario += "MAR-" + inicio + "-" + $("#txtMartesFin").val() + "|";

     inicio = $("#txtMiercolesIni").val();
     if (inicio.length > 0)
          horario += "MIE-" + inicio + "-" + $("#txtMiercolesFin").val() + "|";

     inicio = $("#txtJuevesIni").val();
     if (inicio.length > 0)
          horario += "JUE-" + inicio + "-" + $("#txtJuevesFin").val() + "|";

     inicio = $("#txtViernesIni").val();
     if (inicio.length > 0)
          horario += "VIE-" + inicio + "-" + $("#txtViernesFin").val() + "|";

     inicio = $("#txtSabadoIni").val();
     if (inicio.length > 0)
          horario += "SAB-" + inicio + "-" + $("#txtSabadoFin").val();
     return horario;
}
//LUN HH:mm - HH:mm
function CargaHorarios(horarios) {
     var vec_dias = horarios.split(",");
     var dia;
     var i;
     $("#txtLunesIni").val("");
     $("#txtLunesFin").val("");
     $("#txtMartesIni").val("");
     $("#txtMartesFin").val("");
     $("#txtMiercolesIni").val("");
     $("#txtMiercolesFin").val("");
     $("#txtJuevesIni").val("");
     $("#txtJuevesFin").val("");
     $("#txtViernesIni").val("");
     $("#txtSabadoIni").val("");
     $("#txtSabadoFin").val("");

     for (i = 0; i < vec_dias.length; i++) {
          dia = vec_dias[i].trim().split(" ");
          switch (dia[0]) {
               case "LUN":
                    $("#txtLunesIni").val(dia[1]);
                    $("#txtLunesFin").val(dia[3]);
                    break;
               case "MAR":
                    $("#txtMartesIni").val(dia[1]);
                    $("#txtMartesFin").val(dia[3]);
                    break;
               case "MIE":
                    $("#txtMiercolesIni").val(dia[1]);
                    $("#txtMiercolesFin").val(dia[3]);
                    break;
               case "JUE":
                    $("#txtJuevesIni").val(dia[1]);
                    $("#txtJuevesFin").val(dia[3]);
                    break;
               case "VIE":
                    $("#txtViernesIni").val(dia[1]);
                    $("#txtViernesFin").val(dia[3]);
                    break;
               case "SAB":
                    $("#txtSabadoIni").val(dia[1]);
                    $("#txtSabadoFin").val(dia[3]);
                    break;
          }
     }
}
function LimpiaHorarios() {
     $("#txtLunesIni").val("");
     $("#txtLunesFin").val("");
     $("#txtMartesIni").val("");
     $("#txtMartesFin").val("");
     $("#txtMiercolesIni").val("");
     $("#txtMiercolesFin").val("");
     $("#txtJuevesIni").val("");
     $("#txtJuevesFin").val("");
     $("#txtViernesIni").val("");
     $("#txtSabadoIni").val("");
     $("#txtSabadoFin").val("");
}
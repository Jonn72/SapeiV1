$(document).ready(function () {
     $('#div1').show();
     $('#div2').hide();
     $('#div3').hide();
     $('#div4').hide();
});
function OcultaGraficas()
{
     $('#div1').hide();
     $('#div2').hide();
     $('#div3').hide();
     $('#div4').hide();
}
function CargaDatosEnGraficas()
{
     CargaPastel1();
     CargaPastel2();
     CargaBarras1();
     CargaLineas();
}
$("#cboTipoGrafica").change(function () {
     var valor = $("#cboTipoGrafica option:selected").val();
     $('#div1').hide();
     $('#div2').hide();
     $('#div3').hide();
     $('#div4').hide();
     switch (valor) {
          case "1":
               $('#div1').show();
               CargaPastel1();
               break;
          case "2":
               $('#div2').show();
               CargaPastel2();
               break;
          case "3":
               $('#div3').show();
               CargaBarras1();
               break;
          case "4":
               $('#div4').show();
               CargaLineas();
               break;
     }
});
function CargaPastel1() {
     var echartPieCollapse = echarts.init(document.getElementById('echart_pie2'), theme);

     echartPieCollapse.setOption({
          tooltip: {
               trigger: 'item',
               formatter: "{a} <br/>{b} : {c} ({d}%)"
          },
          legend: {
               x: 'center',
               y: 'bottom',
               data: encabezados
          },
          toolbox: {
               show: true,
               feature: {
                    magicType: {
                         show: true,
                         type: ['pie', 'funnel']
                    },
                    saveAsImage: {
                         show: true,
                         title: "Guardar Imagen"
                    }
               }
          },
          calculable: true,
          series: [{
               type: 'pie',
               radius: [25, 90],
               center: ['50%', 170],
               roseType: 'area',
               x: '50%',
               max: 40,
               sort: 'ascending',
               data: datos
          }]
     });
}
function CargaPastel2() {
     var echartPie = echarts.init(document.getElementById('echart_pie'), theme);

     echartPie.setOption({
          tooltip: {
               trigger: 'item',
               formatter: "{a} <br/>{b} : {c} ({d}%)"
          },
          legend: {
               x: 'center',
               y: 'bottom',
               data: encabezados
          },
          toolbox: {
               show: true,
               feature: {
                    magicType: {
                         show: true,
                         type: ['pie', 'funnel'],
                         option: {
                              funnel: {
                                   x: '25%',
                                   width: '50%',
                                   funnelAlign: 'left',
                                   max: 1548
                              }
                         }
                    },
                    saveAsImage: {
                         show: true,
                         title: "Guardar como Imagen"
                    }
               }
          },
          calculable: true,
          series: [{
               type: 'pie',
               radius: '55%',
               center: ['50%', '48%'],
               data: datos
          }]
     });

     var dataStyle = {
          normal: {
               label: {
                    show: false
               },
               labelLine: {
                    show: false
               }
          }
     };

     var placeHolderStyle = {
          normal: {
               color: 'rgba(0,0,0,0)',
               label: {
                    show: false
               },
               labelLine: {
                    show: false
               }
          },
          emphasis: {
               color: 'rgba(0,0,0,0)'
          }
     };
}
function CargaBarras1() {
     var echartBar = echarts.init(document.getElementById('mainb'), theme);

     echartBar.setOption({
          tooltip: {
               trigger: 'axis'
          },
          legend: {
               data: encabezados
          },
          toolbox: {
               show: false,
               feature: {
                    magicType: {
                         show: true,
                         type: ['pie', 'bar'],
                         option: {
                              funnel: {
                                   x: '25%',
                                   width: '50%',
                                   funnelAlign: 'left',
                                   max: 1548
                              }
                         }
                    },
                    saveAsImage: {
                         show: true,
                         title: "Guardar como Imagen"
                    }
               }
          },
          calculable: true,
          xAxis: [{
               type: 'category',
               axisLabel: {
                    show: true,
                    interval: '0',    // {number}
                    rotate: 30,
                    formatter: '{value}'                  
               },
               data: encabezados
          }],
          yAxis: [{
               type: 'value'
          }],
          series: [{
               name: 'Cantidad',
               type: 'bar',
               data: valores,
               itemStyle: {
                    normal: {
                         label: {
                              show: true,
                              position: 'inside'
                         }
                    }
               }
          }]
     });
}
function CargaLineas()
{
     if ($('#echart_line').length) {

          var echartLine = echarts.init(document.getElementById('echart_line'), theme);

          echartLine.setOption({
               title: {
                    text: 'Gráfica de Líneas'
               },
               tooltip: {
                    trigger: 'axis'
               },
               legend: {
                    x: 220,
                    y: 40,
                    data: encabezados
               },
               toolbox: {
                    show: true,
                    feature: {
                         magicType: {
                              show: true,
                              title: {
                                   line: 'Líneal',
                                   bar: 'Barras',
                              },
                              type: ['line', 'bar']
                         },
                         restore: {
                              show: true,
                              title: "Restaurar"
                         },
                         saveAsImage: {
                              show: true,
                              title: "Guardar Imagen"
                         }
                    }
               },
               calculable: true,
               xAxis: [{
                    type: 'category',
                    boundaryGap: false,
                    axisLabel: {
                         show: true,
                         interval: '0',    // {number}
                         rotate: 30,
                         formatter: '{value}'
                    },
                    data: encabezados
               }],
               yAxis: [{
                    type: 'value'
               }],
               series: [{
                    name: 'Valor',
                    type: 'line',
                    smooth: true,
                    itemStyle: {
                         normal: {
                              areaStyle: {
                                   type: 'default'
                              }
                         }
                    },
                    data: valores,

               }]
          });

     }
}
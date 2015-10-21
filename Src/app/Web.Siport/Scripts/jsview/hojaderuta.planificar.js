$(document).ready(function () {
     
    $("#btnBuscarOrdSrvDisponibles").click(function () {

        var objJQgrid = $("#listaordenserviciogrid");
        var strfecha = $("#FechaOrdenServicioFiltro").val();
        
        if ($.trim(strfecha) == ''){
            alert("Tiene que ingresar la fecha de la orden de servicio");
            return;
        }

        var vUrlData = $(objJQgrid).data("urldata") + "?fordsrv=" + strfecha;
        jQuery(objJQgrid).jqGrid().setGridParam({ url: vUrlData }).trigger("reloadGrid");
        //$(objJQgrid).trigger("reloadGrid");
    });


    $("#btnagregardestino").click(function ()
    {
        var arraymodelo = [];
        var objJQgrid = $("#listaordenserviciogrid");
        var objSelect = $("#IdVehiculoDisponible");
        var objGridPlan = $("#listarutaplanificada")
        var vUrlData = $(this).data("urldata");


        var vIds = $(objJQgrid).getGridParam('selarrrow');
        if ($.trim($(objSelect).val()) == ''){
            alert("Tiene que ingresar un vehiculo disponible");
            return;
        }
        if (!vIds || !vIds[0]){
            alert("Tiene que seleccionar un destino");
            return;
        }

        var vstrid = "";
        $(vIds).each(function () {
            vstrid += "," + this[0];
        });
        if (vstrid.length) vstrid = vstrid.substr(1);
        var textselect = $("#IdVehiculoDisponible option:selected").text();
        var param =
       {
           '[0].Key': 'pIds',
           '[0].Value': vstrid,
           '[1].Key': 'pIdVehiculo',
           '[1].Value': $(objSelect).val(),
           '[2].Key': 'pPlaca',
           '[2].Value': textselect,
           pGuid: $("#IdGuid").val()
       };


        $.ajax({
            url: vUrlData,
            type: 'post',
            dataType: 'json',
            data: param,
            cache: false,
            success: function (result)
            {
                if (result.res) jQuery(objGridPlan).jqGrid().trigger("reloadGrid");
                alert(result.msj);
            },
            error: function (err) {
                alert(err.toString());
            }
        });

    });

   

    configurargridordservicio();
    configurargridrutasplanificadas();
    


});

function configurargridordservicio()
{
    $.jgrid.defaults.width = 780;
    $.jgrid.defaults.responsive = true;
    var grid = $("#listaordenserviciogrid");
    var gridpager = $("#pagelistaordenserviciogrid");
    var vurljson = $(grid).data("jsonurl");

    $(grid).jqGrid({
        url: vurljson,
        datatype: 'json',
        mtype: 'Get',
        colNames: [' ', '', 'Ord. Srv - Destino'],
        colModel:
            [
                { key: true, hidden: true, name: "IdOrdenServicioDestino", index: "IdOrdenServicioDestino" },
                { key: false, hidden: true, name: "DesOrdenServicioDestino", index: "DesOrdenServicioDestino" },
                { key: false, name: "DesOrdenServicioDestino", index: "DesOrdenServicioDestino" }
            ],
        //pager: jQuery(gridpager),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        //autoheight: true,
        viewrecords: true,
        emptyrecords: 'No se encontraron registros',
        jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                id: 0
            },
        autowidth: true,
        height:'100px',
        multiselect: true,
        
    });

}

function configurargridvehiculos()
{
    $.jgrid.defaults.width = 780;
    $.jgrid.defaults.responsive = true;
    var grid = $("#listavehiculos");
    var gridpager = $("#pagelistavehiculos");
    var vurljson = $(grid).data("jsonurl");

    $(grid).jqGrid({
        url: vurljson,
        datatype: 'json',
        mtype: 'Get',
        colNames: [' ', 'Vehiculos Disponibles'],
        colModel:
            [
                { key: true, hidden: true, name: "IdOrdenServicioDestino", index: "IdOrdenServicioDestino" },
                { key: false, name: "Descripcion", index: "Descripcion" }
            ],
        //pager: jQuery(gridpager),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        //autoheight: true,
        viewrecords: true,
        emptyrecords: 'No se encontraron registros',
        jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                id: 0
            },
        autowidth: true,
        height: '100px',
        multiselect: true,

    });
}

function configurargridrutasplanificadas()
{
    $.jgrid.defaults.width = 780;
    $.jgrid.defaults.responsive = true;
    var grid = $("#listarutaplanificada");
    var gridpager = $("#pagelistarutaplanificada");
    var vurljson = $(grid).data("jsonurl");

    $(grid).jqGrid({
        url: vurljson,
        datatype: 'json',
        mtype: 'Get',
        colNames: [' ', 'Vehiculo', 'Orden de Servicio', 'Dirección', 'Horario'],
        colModel:
            [
                { key: true, hidden: true, name: "IdOrdenServicioDestino", index: "IdOrdenServicioDestino" },
                { key: false, name: "PlacaVehiculo", index: "PlacaVehiculo" },
                { key: false, name: "CodigoOrdServicio", index: "CodigoOrdServicio" },
                { key: false, name: "Direccion", index: "Direccion" },
                { key: false, name: "DesHorarioEntrega", index: "DesHorarioEntrega" },

            ],
        pager: jQuery(gridpager),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        autoheight: true,
        viewrecords: true,
        emptyrecords: 'No se encontraron registros',
        jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                id: 0
            },
        autowidth: true,
        //height: '100px',
        multiselect: false,

    });

}
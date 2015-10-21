
$(document).ready(function () {
    jqgrid_view();
    $("#btnagregar").click(function () { btnagregar_onClick(this); });
    $("#btneliminar").click(function () { btneliminar_onclick(this); });

});

function btnagregar_onClick(obj)
{
    var modalcontainer = $('<div id="modalcontainer"></div>').addClass("modal hide fade in").appendTo('body');
    //var modalcontainer = $('<div id="modalcontainer"></div>').appendTo('body');

    var modalpartialview = $('<div id="modalpartial"></div>').appendTo(modalcontainer);
    var urlmodal = $(obj).data("urlmodal");

    $.get(urlmodal, function (data) {

        ////alert(data);
        $(modalpartialview).html(data);
        $(modalcontainer).modal('show');
 

    });

    //modalcontainer.load(urlmodal, function () {
    //    modalcontainer.dialog({
    //        title: 'RUTA',
    //        autoOpen: true,
    //        position: 'center',
    //        width: 440,
    //        height: 'auto',
    //        modal: true,
    //        draggable: true,
    //        resizable: false,
    //        success: function () { },
    //        open: function () { },
    //        close: function () {
    //            $(this).remove();
    //            modalcontainer.dialog("close").css('display', 'none');;
    //            modalcontainer.empty();
    //        }
    //    });
    //});

}

function btneliminar_onclick(obj)
{


}

function jqgrid_view()
{
    $.jgrid.defaults.width = 780;
    $.jgrid.defaults.styleUI = 'Bootstrap';
    $.jgrid.defaults.responsive = true;

    var listadestinosgrid = $("#listadestinosgrid");
    $(listadestinosgrid).jqGrid({
        url: $(listadestinosgrid).data("jsonurl"),
        datatype: 'json',
        mtype: 'Get',
        colNames: [' ', 'Dirección', 'Referencia', 'Ubigeo', 'Fecha de Entrega', 'Horario de Entrega', 'Estado'],
        colModel:
            [
                { key: true, hidden: true, name: 'IdOrdenServicioDestino', index: 'IdOrdenServicioDestino' },
                { key: false, name: 'Direccion', index: 'Direccion', editable: false },
                { key: false, name: 'Referencia', index: 'Referencia', editable: false },
                { key: false, name: 'DescripcionUbigeo', index: 'DescripcionUbigeo', editable: false },
                { key: false, name: 'FechaEstEntrega', index: 'FechaEstEntrega', editable: false, formatter: "date", formatoptions: { srcformat: "ISO8601Long", newformat: "m/d/Y" } },
                { key: false, name: 'DesHorarioEntrega', index: 'DesHorarioEntrega', editable: false },
                { key: false, name: 'Estado', index: 'Estado', editable: false },
            ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        autoheight: true,
        viewrecords: true,
        caption: 'Destinos de la Orden de Servicio',
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
        multiselect: false,
        beforeRequest: function () {
            //responsive_jqgrid(listadestinosgrid);
        },
    });
}

function responsive_jqgrid(jqgrid) {
    jqgrid.find('.ui-jqgrid').addClass('clear-margin span12').css('width', '');
    jqgrid.find('.ui-jqgrid-view').addClass('clear-margin span12').css('width', '');
    jqgrid.find('.ui-jqgrid-view > div').eq(1).addClass('clear-margin span12').css('width', '').css('min-height', '0');
    jqgrid.find('.ui-jqgrid-view > div').eq(2).addClass('clear-margin span12').css('width', '').css('min-height', '0');
    jqgrid.find('.ui-jqgrid-sdiv').addClass('clear-margin span12').css('width', '');
    jqgrid.find('.ui-jqgrid-pager').addClass('clear-margin span12').css('width', '');
}


    

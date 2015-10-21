$(document).ready(function () {
    InizializarGrid();

    $("#btncrearordenservicio").click(function () {
        var vnewurl = $(this).data("newurl");
        $(window).attr("location", vnewurl);
    });

    $("#btnplanificarruta").click(function () {
        var vnewurl = $(this).data("pordserv");
        $(window).attr("location", vnewurl);
    });

});

function InizializarGrid()
{
    $.jgrid.defaults.width = 780;
    $.jgrid.defaults.responsive = true;

    var grid = $("#listaordenserviciogrid");
    var gridpager = $("#pagelistaordenserviciogrid");
    var vurljson = $(grid).data("jsonurl");

    var listadestinosgrid = $(grid);
    $(listadestinosgrid).jqGrid({
        url: $(listadestinosgrid).data("jsonurl"),
        datatype: 'json',
        mtype: 'Get',
        colNames: [' ', 'Código Servicio', 'Descripción', 'Código Cliente', 'Cliente', 'Nro. Destinos', 'Fecha de Creación', 'Usuario Creación'],
        colModel:
            [
                { key: true, hidden: true, name: 'IdOrdenServicio', index: 'IdOrdenServicioDestino' },
                { key: false, name: 'CodigoOrdenServicio', index: 'CodigoOrdenServicio', editable: false },
                { key: false, name: 'Descripcion', index: 'CodigoOrdenServicioDescripcion', editable: false },
                { key: false, name: 'CodigoCliente', index: 'CodigoCliente', editable: false },
                { key: false, name: 'NombreRazonSocial', index: 'NombreRazonSocial', editable: false},
                { key: false, name: 'CantDestinos', index: 'CantDestinos', editable: false },
                { key: false, name: 'FechaCreacion', index: 'FechaCreacion', editable: false, formatter: "date", formatoptions: { srcformat: "ISO8601Long", newformat: "m/d/Y" }  },
                { key: false, name: 'UsuarioCreacion', index: 'UsuarioCreacion', editable: false },
               
            ],
        pager: jQuery(gridpager),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        autoheight: true,
        viewrecords: true,
        //caption: 'Orden de Servicio',
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
        ondblClickRow: function(rowid, iRow, iCol, e){
            var datarow = jQuery(grid).jqGrid("getRowData", rowid);
            var vurledit = $(grid).data("editurl");

            listaordenserviciogrid_onDoubleClick(datarow, vurledit);
        },
        beforeRequest: function () {
            //responsive_jqgrid(listadestinosgrid);
        },
    });

    function listaordenserviciogrid_onDoubleClick(data, url)
    {
        url += "?codigo=" + data.CodigoOrdenServicio;
        $(window).attr("location", url)
    }

}
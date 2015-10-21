
$(document).ready(function () {
    InicializarGrid();

    $("#btneliminar").click(function () { btneliminar_onclick(this); });
    
    
    $('#containermodal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var urlmodal = button.data('urlmodal');

        $.get(urlmodal, function (data) {
            $(".modal-content").html(data);
        });
    })

    $("#btnbuscarcliente").click(function () {
        var vUrl = $(this).data("urltx");
        var inputCodigoCliente = $("#CodigoCliente")
        var inputIdCliente = $("#IdClientes");
        var inputNombreRazonSocial = $("#NombreRazonSocial");

        var valorinput = inputCodigoCliente.val();

        if ($.trim(valorinput) != "" || valorinput != undefined) {
            $.ajax({
                async: false,
                type: "post",
                dataType: 'json',
                url: vUrl,
                data: { pCodigoCliente: valorinput },
                cache: false,
                success: function (data) {
                    if (data.res) {
                        $(inputIdCliente).val(data.obj.IdClientes);
                        $(inputNombreRazonSocial).val(data.obj.NombreRazonSocial);
                    }
                },
                error: function (err) {
                    alert("Ocurrio un Error");
                }
            });
        }

    });

});

function orderserviciodestinograbar_onclick(obj)
{
    var pdata = {
        CodigoUbigeo: $("#CodigoUbigeo").val(),
        FechaEstEntrega: $("#FechaEstEntrega").val(),
        IdHorarioEntrega: $("#IdHorarioEntrega").val(),
        Direccion: $("#Direccion").val(),
        Referencia: $("#Referencia").val(),
        Latitude: $("#Latitude").val(),
        Longitude: $("#Longitude").val()
    }
    var urltx = $(obj).data("urltx");
    var pguid = $("#IdGuid").val();

    //$('#Errores').css("display", "none");
    //var listaErrores = $("#ListaErrores");
    //listaErrores.empty();

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: urltx,
        data: { pdata: JSON.stringify(pdata), pguid: pguid },
        cache: false,
        success: function (result)
        {
            //jQuery("#GridBuscarDimensionRes").jqGrid().setGridParam({ url: vUrl }).trigger("reloadGrid");
            if (result.res) $("#listadestinosgrid").trigger("reloadGrid");
            else alert(result.mensaje);
        },
        error: function (err) {
            alert(err.toString());
        }
    });

}



function dbldepartamento_onChange(obj){
    dropdowlistubigeo_onChange(obj, "P");
    dropdowlistubigeo_onChange(obj, "V");
}

function dplprovincia_onChange(obj) {
    dropdowlistubigeo_onChange(obj, "V");
}

function dpldistrito_onChange(obj) {
    $("#CodigoUbigeo").val($("#dpldistrito").val());
}

function dropdowlistubigeo_onChange(obj, tipo)
{
    var vUrl = $("#divubigeo").data("urlubigeo");
    var codigoubigeo = $(obj).val();
    var dropdownlist = obj;
    if (tipo == "D") dropdownlist = $("#dpldepartamento");
    else if (tipo == "P") dropdownlist = $("#dplprovincia");
    else if (tipo == "V") dropdownlist = $("#dpldistrito");
    else return;

    dropdownlist.empty();
    dropdownlist.append($("<option />").val("").text("[Seleccionar Item]"));

    if (codigoubigeo == "") {
        return;
    }

    $.ajax({
        async: false,
        type: "post",
        dataType: 'json',
        url: vUrl,
        data: { pCodigoUbigeo: codigoubigeo },
        cache: false,
        success: function (data) {
            if (data.res) {
                $.each(data.lista, function () {
                    dropdownlist.append($("<option />").val(this.CodigoUbigeo).text(this.Descripcion));
                });
            }
        },
        error: function (err) {
            alert("Ocurrio un Error");
        }
    });



}

function btneliminar_onclick(obj)
{


}

function InicializarGrid()
{
    $.jgrid.defaults.width = 780;
    //$.jgrid.defaults.styleUI = 'Bootstrap';
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




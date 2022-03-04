OcorrenciasProximasService = function () {
    var service = {};

    service.buscarOcorrencias = function (coordenadas) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Ocorrencia/BuscarOcorrenciasProximas";

        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(coordenadas),
            contentType: "application/json; charset=utf-8",
            success: function (retorno) {
                if (retorno.sucesso == true) {
                    dfd.resolve(retorno.dados);
                }
                else {
                    dfd.reject(retorno.mensagem);
                }
            },
            error: function () {
                dfd.reject("Não foi possível buscar as ocorrências próximas a você.")
            }
        });

        return dfd.promise();
    }

    return service;
}

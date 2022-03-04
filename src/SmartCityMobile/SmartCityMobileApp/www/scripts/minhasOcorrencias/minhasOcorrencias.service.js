MinhasOcorrenciasService = function () {
    var service = {};

    service.consultar = function (usuario) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Ocorrencia/BuscarOcorrenciasPorUsuario";

        $.ajax({
            type: "GET",
            url: urlApi,
            data: "idUsuario=" + usuario,
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
                dfd.reject("Não foi possível buscar as suas ocorrências.")
            }
        });

        return dfd.promise();
    }

    return service;
}

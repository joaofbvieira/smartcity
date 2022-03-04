CriarOcorrenciaService = function () {
    var service = {};

    service.gravarOcorrencia = function (dadosOcorrencia) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Ocorrencia/Adicionar";

        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(dadosOcorrencia),
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
                dfd.reject("Não foi possível salvar a ocorrência.")
            }
        });

        return dfd.promise();
    }

    return service;
}

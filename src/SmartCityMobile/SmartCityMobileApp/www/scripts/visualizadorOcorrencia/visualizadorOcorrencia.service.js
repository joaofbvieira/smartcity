VisualizadorOcorrenciaService = function () {
    var service = {};

    service.obterDados = function (idOcorrencia) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Ocorrencia/BuscarPorId";

        $.ajax({
            type: "GET",
            url: urlApi,
            data: "id=" + idOcorrencia,
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
                dfd.reject("Não foi possível buscar os dados da ocorrência.")
            }
        });

        return dfd.promise();
    }


    service.votarOuDenunciar = function (dados) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Ocorrencia/VotarEmOcorrencia";

        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(dados),
            contentType: "application/json; charset=utf-8",
            success: function (retorno) {
                if (retorno.sucesso == true) {
                    dfd.resolve(true);
                }
                else {
                    dfd.reject(retorno.mensagem);
                }
            },
            error: function () {
                dfd.reject("Não foi possível votar na ocorrência.")
            }
        });

        return dfd.promise();
    }
    
    return service;
}



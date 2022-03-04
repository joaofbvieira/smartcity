ResetarSenhaUsuarioService = function () {
    var service = {};

    service.resetarSenha = function (dadosEnvio) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Usuario/RecuperarSenha";

        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(dadosEnvio),
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
                dfd.reject("Não foi possível recuperar a senha.")
            }
        });

        return dfd.promise();
    }

    return service;
}

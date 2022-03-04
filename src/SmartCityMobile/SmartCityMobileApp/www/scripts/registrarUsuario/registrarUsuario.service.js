RegistrarUsuarioService = function () {
    var service = {};

    service.gravarUsuario = function (dadosUsuario) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Usuario/Registrar";

        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(dadosUsuario),
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
                dfd.reject("Não foi possível efetuar o registro.")
            }
        });

        return dfd.promise();
    }

    return service;
}

LoginService = function () {
    var service = {};
    
    service.autenticarUsuario = function (dadosAuth) {
        var dfd = $.Deferred();
        var urlApi = localStorage["ip_servidor_smart"] + "/api/Usuario/Autenticar";
        console.log(urlApi);
        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(dadosAuth),
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
                dfd.reject("Não foi possível autenticar o usuário.")
            }
        });
        
        return dfd.promise();
    };
    
    return service;
}

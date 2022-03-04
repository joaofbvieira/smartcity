var ResetarSenhaUsuarioController = function () {
    var controller = {
        self: null,
        initialize: function () {
            self = this;
            self.resetarSenhaUsuarioService = new ResetarSenhaUsuarioService();
            self.bindEvents();
        },

        bindEvents: function () {
            $("#btnResetarSenha").on('click', this.onBtnResetarSenhaClick);
        },

        onBtnResetarSenhaClick: function () {
            var dados = new Object();
            dados.Email = $("#email_recuperacao").val();

            if (dados.Email === undefined || dados.Email === null || dados.Email === "") {
                navigator.notification.alert("Preencha o seu e-mail.");
                return;
            }

            exibirLoading();
            var result = self.resetarSenhaUsuarioService.resetarSenha(dados);

            result.done(function (retorno) {
                esconderLoading();
                navigator.notification.alert("A nova senha foi enviada para o seu e-mail.");

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });
        }

    }
    controller.initialize();
    return controller;
}
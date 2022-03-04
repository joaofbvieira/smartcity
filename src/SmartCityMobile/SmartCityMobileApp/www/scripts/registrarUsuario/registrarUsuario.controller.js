var RegistrarUsuarioController = function () {
    var controller = {
        self: null,
        initialize: function () {
            self = this;
            self.registrarUsuarioService = new RegistrarUsuarioService();
            self.bindEvents();
        },

        bindEvents: function () {
            $("#btnSalvar").on('click', this.onBtnSalvarClick);
        },

        onBtnSalvarClick: function () {
            var dados = self.obterDadosFoFormulario();

            if (!self.validarDadosFormulario(dados)) {
                navigator.notification.alert("Preencha todos os campos.");
                return;
            }
            
            exibirLoading();
            var result = self.registrarUsuarioService.gravarUsuario(dados);

            result.done(function (retorno) {
                esconderLoading();
                localStorage.setItem("usuario_smartcity", dados.Email);
                localStorage.setItem("senha_smartcity", dados.Senha);
                localStorage.setItem("mensagem_outra_pagina", "Parabéns, você está cadastrado no SmartCity. Foi enviado um e-mail para você com suas informações de acesso.");
                localStorage.setItem("realizouLogoff", "true");
                localStorage.setItem("realizar_login_automatico", "false");
                window.location.href = "login.html";

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });

        },

        obterDadosFoFormulario: function () {
            var dados = new Object();
            dados.Nome = $("#nome").val();
            dados.Email = $("#email").val();
            dados.Senha = $("#senha").val();
            dados.ConfirmacaoSenha = $("#confirma_senha").val();
            dados.CidadeId = $("#cidade_id").val();
            return dados;
        },

        validarDadosFormulario: function (dadosForm) {
            if (dadosForm.Nome == "")
                return false;

            if (dadosForm.Email == "")
                return false;

            if (dadosForm.CidadeId == "")
                return false;

            if (dadosForm.Senha == "")
                return false;

            if (dadosForm.ConfirmacaoSenha == "")
                return false;

            return true;
        }

    }
    controller.initialize();
    return controller;
}
var LoginController = function () {
    var controller = {
        self: null,
        initialize: function () {
            self = this;
            self.efetuarLoginAutomatico = true;
            self.loginService = new LoginService();
            self.bindEvents();
        },

        bindEvents: function () {
            $('#btnAutenticar').on('click', this.onBtnAutenticarClick);
            $('#btnRegistrar').on('click', this.onBtnRegistrarClick);
            $('#linkResetSenha').on('click', this.onLinkResetSenhaClick);
        },

        onBtnAutenticarClick: function (e) {
            e.preventDefault();
            if ($(this).hasClass('active')) {
                return;
            }

            self.salvarIpServidorStorage();
            var user = $("#email_login").val();
            var pass = $("#senha_login").val();

            self.realizarLogin(user, pass);
        },

        onBtnRegistrarClick: function (e) {
            e.preventDefault();
            self.salvarIpServidorStorage();
            window.location.href = "registrarUsuario.html";
        },

        realizarLogin: function (login, senha) {
            $('#btnAutenticar').addClass('active');

            if (!login || !senha) {
                navigator.notification.alert("Informe seu e-mail e senha para acessar o SmartCity.");
                $('#btnAutenticar').removeClass('active');
                return;
            }
            else {
                exibirLoading();

                var dadosAutenticacao = new Object();
                dadosAutenticacao.Email = login;
                dadosAutenticacao.Senha = senha;

                var result = self.loginService.autenticarUsuario(dadosAutenticacao);

                result.done(function (retorno) {
                    esconderLoading();
                    self.armazenarDadosLogin(login, senha);

                    window.localStorage["realizar_login_automatico"] = true;
                    window.localStorage["usuario_atual"] = JSON.stringify(retorno);
                    
                    $('#btnAutenticar').removeClass('active');
                    window.location.href = "menuPrincipal.html";

                }).fail(function (error) {
                    esconderLoading();
                    navigator.notification.alert(error);
                    $('#btnAutenticar').removeClass('active');
                });
            }
        },
        
        armazenarDadosLogin: function (login, senha) {
            window.localStorage["lembrar_login_smartcity"] = $("#chkLembrarDados").is(':checked');

            if ($("#chkLembrarDados").is(':checked')) {
                window.localStorage["usuario_smartcity"] = login;
                window.localStorage["senha_smartcity"] = senha;
            }
            else {
                window.localStorage.removeItem("usuario_smartcity");
                window.localStorage.removeItem("senha_smartcity");
            }
        },

        autenticarComCredenciaisSalvas: function () {
            if (window.localStorage["usuario_smartcity"] != undefined && window.localStorage["usuario_smartcity"] != undefined) {
                
                if (window.localStorage["realizouLogoff"] != undefined && window.localStorage["realizouLogoff"] === "true") {
                    self.efetuarLoginAutomatico = false;
                    window.localStorage.removeItem("usuario_atual");
                    window.localStorage["realizar_login_automatico"] = false;
                    window.localStorage.removeItem("realizouLogoff");
                }

                var user = window.localStorage["usuario_smartcity"];
                var pass = window.localStorage["senha_smartcity"];

                if (self.efetuarLoginAutomatico === true) {
                    self.realizarLogin(user, pass);
                }
                else {
                    $("#email_login").val(user);
                    $("#senha_login").val(pass);
                }
            }
        },

        inicializarComponentes: function () {
            if (window.localStorage["lembrar_login_smartcity"] != undefined && window.localStorage["lembrar_login_smartcity"] === "false") {
                $("#chkLembrarDados").click();
            }

            if (window.localStorage["realizar_login_automatico"] != undefined && window.localStorage["realizar_login_automatico"] === "false") {
                self.efetuarLoginAutomatico = false;
            }

            if (window.localStorage["mensagem_outra_pagina"] != undefined && window.localStorage["mensagem_outra_pagina"] != "") {
                navigator.notification.alert(window.localStorage["mensagem_outra_pagina"]);
                window.localStorage.removeItem("mensagem_outra_pagina");
            }

            if (window.localStorage["ip_servidor_smart"] != undefined && window.localStorage["ip_servidor_smart"] != "") {
                $("#ip_servidor").val(window.localStorage["ip_servidor_smart"]);
            }

            self.salvarIpServidorStorage();
        },

        onLinkResetSenhaClick: function (e) {
            e.preventDefault();
            self.salvarIpServidorStorage();
            window.location.href = "resetarSenhaUsuario.html";
        },

        salvarIpServidorStorage: function () {
            window.localStorage.setItem("ip_servidor_smart", $("#ip_servidor").val());
        }
        
    }
    controller.initialize();
    return controller;
}
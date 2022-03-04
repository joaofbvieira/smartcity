var MenuPrincipalController = function () {
    var controller = {
        self: null,
        initialize: function () {
            self = this;
            self.bindEvents();
        },

        bindEvents: function () {
            $('#btnCriarOcorrencia').on('click', this.onBtnCriarOcorrenciaClick);
            $('#btnMinhasOcorrencias').on('click', this.onBtnMinhasOcorrenciasClick);
            $('#btnOcorrenciasProximas').on('click', this.onBtnOcorrenciasProximasClick);
            $('#btnLogoffApp').on('click', this.onBtnLogoffClick);
        },

        onBtnCriarOcorrenciaClick: function (e) {
            e.preventDefault();
            window.location.href = "criarOcorrencia.html";
        },

        onBtnMinhasOcorrenciasClick: function (e) {
            e.preventDefault();
            window.location.href = "minhasOcorrencias.html";
        },

        onBtnOcorrenciasProximasClick: function (e) {
            e.preventDefault();
            window.location.href = "ocorrenciasProximas.html";
        },

        onBtnLogoffClick: function(e) {
            e.preventDefault();
            localStorage.setItem("realizouLogoff", "true");
            localStorage.setItem("realizar_login_automatico", "false");
            window.location.href = "login.html";
        },

        inicializarComponentes: function () {
            if (window.localStorage["mensagem_outra_pagina"] != undefined && window.localStorage["mensagem_outra_pagina"] != "") {
                navigator.notification.alert(window.localStorage["mensagem_outra_pagina"]);
                window.localStorage.removeItem("mensagem_outra_pagina");
            }
        }

    }
    controller.initialize();
    return controller;
}
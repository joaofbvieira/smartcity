var VisualizadorOcorrenciaController = function () {
    var controller = {
        self: null,
        idOcorrencia: null,
        paginaOrigem: null,

        initialize: function () {
            self = this;
            self.visualizadorOcorrenciaService = new VisualizadorOcorrenciaService();
            self.storage = new VisualizadorOcorrenciaStorage();
            self.carregarInformacoesStorage();
            self.bindEvents();
            self.obterDadosOcorrencia();
        },

        bindEvents: function () {
            $('#btnVisualizarFoto').on('click', this.onBtnVisualizarFotoClick);
            $('#btnFecharImagem').on('click', this.onClickFecharVisualizacaoFoto);
            $('#btnVotar').on('click', this.onBtnVotarClick);
            $('#btnDenunciar').on('click', this.onBtnDenunciarClick);
            $('#btnSalvarConfirmacaoDenuncia').on('click', this.onBtnConfirmarDenunciaClick);
            $('#btnCancelarConfirmacaoDenuncia').on('click', this.onBtnCancelarDenunciaClick);
            $("#motivoDenuncia").change(this.onSelecionarMotivoDenuncia);
        },

        carregarInformacoesStorage: function () {
            self.idOcorrencia = self.storage.getOcorrenciaId();
            self.paginaOrigem = self.storage.getUrlOrigem();

            self.storage.delOcorrenciaStorage();
        },
        
        obterDadosOcorrencia: function () {
            exibirLoading();
            var result = self.visualizadorOcorrenciaService.obterDados(self.idOcorrencia);

            result.done(function (retorno) {
                esconderLoading();
                self.renderizarOcorrenciaNaTela(retorno);

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });
        },

        onBtnVisualizarFotoClick: function () {
            $("#exibeFotoTirada").show();
        },

        onClickFecharVisualizacaoFoto: function () {
            $("#exibeFotoTirada").hide();
        },

        onBtnVotarClick: function () {
            navigator.notification.confirm(
                'Deseja Votar nesta ocorrência?',
                onConfirmarVotacao,
                'Votar',
                ['Sim', 'Não']
            );

            function onConfirmarVotacao(buttonIndex) {
                if (buttonIndex === 1) {
                    var usuarioAtual = JSON.parse(localStorage["usuario_atual"]);

                    var dadosVotacao = new Object();
                    dadosVotacao.OcorrenciaId = self.idOcorrencia;
                    dadosVotacao.Positivo = true;
                    dadosVotacao.UsuarioId = usuarioAtual.usuarioId;
                    dadosVotacao.Comentario = "";
                    
                    exibirLoading();
                    var result = self.visualizadorOcorrenciaService.votarOuDenunciar(dadosVotacao);

                    result.done(function (retorno) {
                        esconderLoading();
                        if (retorno) {
                            navigator.notification.alert('A ocorrência foi votada.');
                            self.onBtnCancelarDenunciaClick();
                            $('#btnVotar').hide();
                            $('#btnDenunciar').hide();
                        }
                        else {
                            navigator.notification.alert('Não foi possível votar na ocorrência.');
                        }
                    }).fail(function (error) {
                        esconderLoading();
                        navigator.notification.alert(error);
                    });

                }
            }
        },
        
        onBtnDenunciarClick: function () {
            $("#motivoDenuncia").val(0).change();
            $("#modalDenuncia").fadeIn();
        },

        onBtnConfirmarDenunciaClick: function () {
            var usuarioAtual = JSON.parse(localStorage["usuario_atual"]);

            var dadosDenuncia = new Object();
            dadosDenuncia.OcorrenciaId = self.idOcorrencia;
            dadosDenuncia.Positivo = false;
            dadosDenuncia.UsuarioId = usuarioAtual.usuarioId;
            dadosDenuncia.Comentario = $("#textoOutroMotivo").val();
            dadosDenuncia.MotivoDenunciaId = $("#motivoDenuncia").val();
            
            if (dadosDenuncia.idMotivo === "0") {
                navigator.notification.alert('Selecione um motivo.');
                return false;
            }
            else if (dadosDenuncia.idMotivo === "9" && (dadosDenuncia.complemento === undefined || dadosDenuncia.complemento === null || dadosDenuncia.complemento === "")) {
                navigator.notification.alert('Informe o seu motivo.');
                return false;
            }

            exibirLoading();
            var result = self.visualizadorOcorrenciaService.votarOuDenunciar(dadosDenuncia);

            result.done(function (retorno) {
                esconderLoading();
                if (retorno) {
                    navigator.notification.alert('A ocorrência foi denunciada.');
                    self.onBtnCancelarDenunciaClick();
                    $('#btnVotar').hide();
                    $('#btnDenunciar').hide();
                }
                else {
                    navigator.notification.alert('Não foi possível denunciar a ocorrência.');
                }
            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });
        },
        
        onSelecionarMotivoDenuncia: function () {
            if ($("#motivoDenuncia").val() === "9") {
                self.exibirCampoOutroMotivo();
            }
            else {
                self.limparCampoOutroMotivo();
                self.ocultarCampoOutroMotivo();
            }
        },
        
        onBtnCancelarDenunciaClick: function () {
            $("#modalDenuncia").fadeOut();
            self.limparCampoOutroMotivo();
            self.ocultarCampoOutroMotivo();
        },

        limparCampoOutroMotivo: function () {
            $("#textoOutroMotivo").val("");
        },

        exibirCampoOutroMotivo: function () {
            $("#outroMotivoContainer").show();
        },

        ocultarCampoOutroMotivo: function () {
            $("#outroMotivoContainer").hide();
        },


        renderizarOcorrenciaNaTela: function (dados) {
            $("#data_hora").val(dados.data + " - " + dados.hora);
            $("#complemento_informacoes").val(dados.descricao);
            $('#pLocalizacao').text(dados.enderecoCompleto);

            if (dados.imagem != undefined && dados.imagem != null)
                $("#imgFotoOcorrencia").attr("src", "data:image/" + dados.imagem.extensao + ";base64," + dados.imagem.conteudo);
            else
                $("#btnVisualizarFoto").hide();


            var usuarioAtual = JSON.parse(localStorage["usuario_atual"]);

            if (dados.qtdeDenuncias > 0 || dados.qtdeVotos > 0 || dados.usuarioId === usuarioAtual.usuarioId) {
                $("#btnVotar").hide();
                $("#btnDenunciar").hide();
            }
        }


    }
    controller.initialize();
    return controller;
}
var MinhasOcorrenciasController = function () {
    var controller = {
        self: null,
        initialize: function () {
            self = this;
            self.urlPagina = "minhasOcorrencias.html";
            self.minhasOcorrenciasService = new MinhasOcorrenciasService();
            self.visualizadorOcorrenciaStorage = new VisualizadorOcorrenciaStorage();
            self.bindEvents();
        },

        bindEvents: function () {
            $(document).on('click', '.linkVisualizarOcorrencia', this.visualizarOcorrencia);
        },

        consultarOcorrencias: function () {
            var usuarioAtual = JSON.parse(localStorage["usuario_atual"]);

            exibirLoading();

            var result = self.minhasOcorrenciasService.consultar(usuarioAtual.usuarioId);

            result.done(function (retorno) {
                esconderLoading();
                self.renderizarOcorrenciasNaTela(retorno);

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });
        },


        renderizarOcorrenciasNaTela: function (retorno) {

            if (retorno.pendentes.length > 0) {
                $("#listaOcorrencias").append(
                    `
<li data-role="list-divider" role="heading" data-theme="a" class="ui-li-divider ui-bar-a ui-li-has-count">
    Abertas
    <span class="ui-li-count ui-body-inherit" data-theme="c">` + retorno.pendentes.length + `</span>
</li>`);

                $.each(retorno.pendentes, function (index, ocorrencia) {
                    $("#listaOcorrencias").append(self.obterHtmlOcorrencia(ocorrencia));
                });
            }


            if (retorno.atendidas.length > 0) {
                $("#listaOcorrencias").append(
                    `
<li data-role="list-divider" role="heading" data-theme="a" class="ui-li-divider ui-bar-a ui-li-has-count">
    Atendidas
    <span class="ui-li-count ui-body-inherit" data-theme="c">` + retorno.atendidas.length + `</span>
</li>`);

                $.each(retorno.atendidas, function (index, ocorrencia) {
                    $("#listaOcorrencias").append(self.obterHtmlOcorrencia(ocorrencia));
                });
            }


            if (retorno.denunciadas.length > 0) {
                $("#listaOcorrencias").append(
                    `
<li data-role="list-divider" role="heading" data-theme="a" class="ui-li-divider ui-bar-a ui-li-has-count">
    Denunciadas
    <span class="ui-li-count ui-body-inherit" data-theme="c">` + retorno.denunciadas.length + `</span>
</li>`);

                $.each(retorno.denunciadas, function (index, ocorrencia) {
                    $("#listaOcorrencias").append(self.obterHtmlOcorrencia(ocorrencia));
                });
            }

        },

        obterHtmlOcorrencia: function (ocorrencia) {
            return `
<li>
    <a href="#" class="ui-listview-item-button ui-button linkVisualizarOcorrencia ui-btn ui-btn-icon-right ui-icon-carat-r" data-ocorrencia-id="` + ocorrencia.ocorrenciaId + `">
        <h3>` + ocorrencia.data + ` - ` + ocorrencia.hora + `</h3>
        <p>` + ocorrencia.enderecoCompleto + `</p>
        <p>` + ocorrencia.descricao + `</p>
    </a>
</li>
`;
        },
        
        visualizarOcorrencia: function () {
            var ocorrenciaId = $(this).data('ocorrencia-id');
            self.visualizadorOcorrenciaStorage.addOcorrenciaStorage(ocorrenciaId, self.urlPagina);
            window.location = "visualizadorOcorrencia.html";
        }
        
    }
    controller.initialize();
    return controller;
}
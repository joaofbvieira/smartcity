var VisualizadorOcorrenciaStorage = function () {
    var visualizadorStorage = {
        selfStorage: null,
        keyIdOcorrencia: null,
        keyUrlOrigem: null,

        initialize: function () {
            selfStorage = this;
            this.keyIdOcorrencia = "smartcity-visualizacao-ocorrencia-id";
            this.keyUrlOrigem = "smartcity-visualizacao-url-voltar";
        },

        addOcorrenciaStorage: function (idOcorrencia, urlOrigem) {
            selfStorage.delOcorrenciaStorage();
            localStorage.setItem(selfStorage.keyIdOcorrencia, idOcorrencia);
            localStorage.setItem(selfStorage.keyUrlOrigem, urlOrigem);
        },
        
        delOcorrenciaStorage: function () {
            localStorage.removeItem(selfStorage.keyIdOcorrencia);
            localStorage.removeItem(selfStorage.keyUrlOrigem);
        },

        getOcorrenciaId: function () {
            return localStorage[selfStorage.keyIdOcorrencia];
        },

        getUrlOrigem: function () {
            return localStorage[selfStorage.keyUrlOrigem];
        }

    }
    visualizadorStorage.initialize();
    return visualizadorStorage;
}

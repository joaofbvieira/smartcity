var OcorrenciasProximasController = function () {
    var controller = {
        self: null,
        mapa: null,
        latitudeAtual: null,
        longitudeAtual: null,
        raioKmFiltro: null,
        initialize: function () {
            self = this;
            self.raioKmFiltro = 5;   // valor padrão de 5 KM para busca
            self.urlPagina = "ocorrenciasProximas.html";
            self.ocorrenciasProximasService = new OcorrenciasProximasService();
            self.visualizadorOcorrenciaStorage = new VisualizadorOcorrenciaStorage();
            self.bindEvents();
        },

        bindEvents: function () {
            $("#sliderKm").on("slidestop", this.onSlideKmSelecionado);
            $(document).on('click', '.linkMarkerOcorrenciaMapa', this.visualizarOcorrencia);
        },

        onSlideKmSelecionado: function (event, ui) {
            var kmSelecionado = $("#sliderKm").val();
            
            if (kmSelecionado != self.raioKmFiltro) {
                self.raioKmFiltro = kmSelecionado;
                self.buscarOcorrencias();
            }
        },

        inicializarComponentes: function () {
            exibirLoading();
            var resultGeolocation = self.buscarGeolocalizacaoAtual();

            resultGeolocation.done(function () {
                esconderLoading();
                self.buscarOcorrencias();

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
                return;
            });
        },
        
        buscarGeolocalizacaoAtual: function () {
            var dfd = $.Deferred();
            
            var options = { timeout: 30000, enableHighAccuracy: true };
            navigator.geolocation.getCurrentPosition(GetPosition, PositionError, options);

            function GetPosition(position) {
                self.latitudeAtual = position.coords.latitude;
                self.longitudeAtual = position.coords.longitude;
                
                self.renderizarMapa(new google.maps.LatLng(self.latitudeAtual, self.longitudeAtual));
                dfd.resolve(true);
            }

            function PositionError() {
                dfd.reject("Não foi possível buscar o seu endereço atual.");
            }

            return dfd.promise();
        },

        buscarOcorrencias: function () {
            exibirLoading();

            var dadosEnvio = new Object();
            dadosEnvio.RaioKm = self.raioKmFiltro;
            dadosEnvio.Latitude = self.latitudeAtual;
            dadosEnvio.Longitude = self.longitudeAtual;
            
            var result = self.ocorrenciasProximasService.buscarOcorrencias(dadosEnvio);

            result.done(function (retorno) {
                esconderLoading();
                self.renderizarOcorrenciasNoMapa(retorno);

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });
        },

        renderizarMapa: function (latlng) {
            var myOptions = {
                zoom: 13,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var pageHeight = $(document).innerHeight();
            var offsetFromTop = $("#mapaOcorrenciasProximas").offset().top;
            $("#mapaOcorrenciasProximas").height((pageHeight - offsetFromTop) * 0.9);
            
            self.mapa = new google.maps.Map(document.getElementById("mapaOcorrenciasProximas"), myOptions);
            self.adicionarMarcadorNoMapa(latlng, "Você está aqui!", 0);
        },

        renderizarOcorrenciasNoMapa: function (retorno) {
            $.each(retorno.ocorrencias, function (index, ocorrencia) {
                var latLng = new google.maps.LatLng(ocorrencia.latitude, ocorrencia.longitude)
                self.adicionarMarcadorNoMapa(latLng, ocorrencia.descricao, ocorrencia.ocorrenciaId);
            });

        },

        adicionarMarcadorNoMapa: function (latlng, descricao, idOcorrencia) {
            var marker = new google.maps.Marker({
                position: latlng,
                map: self.mapa,
                title: descricao
            });

            if (idOcorrencia > 0) {
                var infowindow = new google.maps.InfoWindow({
                    content: '<a href="#" class="linkMarkerOcorrenciaMapa" data-ocorrencia-id="' + idOcorrencia + '">Clique aqui para visualizar</b>'
                });

                marker.addListener('click', function () {
                    infowindow.open(marker.get('map'), marker);
                });
            }
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
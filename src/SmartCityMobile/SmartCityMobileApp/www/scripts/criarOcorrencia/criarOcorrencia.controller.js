var CriarOcorrenciaController = function () {
    var controller = {
        self: null,
        base64FotoTirada: null,
        geolocalizacaoLatitude: null,
        geolocalizacaoLongitude: null,
        initialize: function () {
            self = this;
            self.criarOcorrenciaService = new CriarOcorrenciaService();
            self.bindEvents();
        },

        bindEvents: function () {
            $('#btnSalvarOcorrencia').on('click', this.onBtnSalvarClick);
            $('#divFotografarOcorrencia').on('click', this.onBtnTirarFotoClick);
        },

        onBtnTirarFotoClick: function () {
            self.base64FotoTirada = null;
            $("#fotoTirada").attr("src", "");
            $("#divContainerFoto").hide();

            navigator.camera.getPicture(onFotoSuccess, onFotoFail, {
                quality: 50,
                destinationType: Camera.DestinationType.DATA_URL
            });

            function onFotoSuccess(imageData) {
                self.base64FotoTirada = imageData;
                $("#fotoTirada").attr("src", "data:image/jpeg;base64," + imageData);
                $("#divContainerFoto").show();
            }

            function onFotoFail(message) {
                console.error('Não foi possível tirar a foto: ' + message);
            }
        },
        
        onBtnSalvarClick: function (e) {
            e.preventDefault();
            
            if (!self.validarFormulario())
                return false;

            var usuarioAtual = JSON.parse(localStorage["usuario_atual"]);

            var dadosForm = new Object();
            dadosForm.Descricao = $("#complemento_informacoes").val();
            dadosForm.UsuarioId = usuarioAtual.usuarioId;
            dadosForm.Latitude = self.geolocalizacaoLatitude;
            dadosForm.Longitude = self.geolocalizacaoLongitude;
            dadosForm.EnderecoCompleto = $('#pLocalizacao').text();

            dadosForm.Imagem = new Object();
            dadosForm.Imagem.Conteudo = self.base64FotoTirada;
            dadosForm.Imagem.Extensao = "jpeg";
            
            self.gravarOcorrencia(dadosForm);
        },
        
        gravarOcorrencia: function (dados) {
            exibirLoading();

            var result = self.criarOcorrenciaService.gravarOcorrencia(dados);

            result.done(function (retorno) {
                esconderLoading();
                localStorage.setItem("mensagem_outra_pagina", "A ocorrência foi gravada com sucesso.");
                window.location.href = "menuPrincipal.html";

            }).fail(function (error) {
                esconderLoading();
                navigator.notification.alert(error);
            });
        },

        buscarGeolocalizacaoAtual: function () {
            var options = { timeout: 30000, enableHighAccuracy: true };
            navigator.geolocation.getCurrentPosition(GetPosition, PositionError, options);

            function GetPosition(position) {
                self.geolocalizacaoLatitude = position.coords.latitude;
                self.geolocalizacaoLongitude = position.coords.longitude;
                ReverseGeocode(self.geolocalizacaoLatitude, self.geolocalizacaoLongitude);
            }

            function PositionError() {
                navigator.notification.alert('Não foi possível buscar o seu endereço atual.');
            }

            function ReverseGeocode(latitude, longitude) {
                var reverseGeocoder = new google.maps.Geocoder();
                var currentPosition = new google.maps.LatLng(latitude, longitude);
                reverseGeocoder.geocode({ 'latLng': currentPosition }, function (results, status) {

                    if (status === google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            self.exibirEnderecoAtual(results[0].formatted_address);
                        }
                        else {
                            navigator.notification.alert('Não foi possível buscar o seu endereço atual.');
                        }
                    } else {
                        navigator.notification.alert('Não foi possível buscar o seu endereço atual.');
                    }
                });
            }
        },

        exibirEnderecoAtual: function (endereco) {
            $('#pLocalizacao').text(endereco);
        },

        validarFormulario: function () {
            if (self.base64FotoTirada === undefined || self.base64FotoTirada === null) {
                navigator.notification.alert("Tire uma foto para usar de evidência na sua ocorrência.")
                return false;
            }

            if (self.geolocalizacaoLatitude === null || self.geolocalizacaoLongitude === null) {
                navigator.notification.alert("Nao foi possível detectar a sua localização.")
                return false;
            }
            return true;
        }

    }
    controller.initialize();
    return controller;
}
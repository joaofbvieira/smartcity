var controller = null;

var app = {
    // Application Constructor
    initialize: function () {
        if (navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry)/)) {
            document.addEventListener("deviceready", this.onDeviceReady, false);
        } else {
            this.onDeviceReady();
        }
    },

    onDeviceReady: function () {
        controller = new OcorrenciasProximasController();
        controller.inicializarComponentes();
    },
};
app.initialize();

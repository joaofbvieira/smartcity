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
        StatusBar.hide();
        controller = new LoginController();
        controller.inicializarComponentes();
        controller.autenticarComCredenciaisSalvas();
    },
};
app.initialize();

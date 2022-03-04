var exibirLoading = (function () {
    $(".smartcity-loader-container").show();
});

var esconderLoading = (function () {
    $(".smartcity-loader-container").hide();
});

var voltarParaPaginaAnterior = (function () {
    var userAgent = navigator.userAgent || navigator.vendor || window.opera;
    if (userAgent.match(/iPad/i) || userAgent.match(/iPhone/i) || userAgent.match(/iPod/i)) {
        // IOS DEVICE
        history.go(-1);
    } else if (userAgent.match(/Android/i)) {
        // ANDROID DEVICE
        navigator.app.backHistory();
    } else {
        // EVERY OTHER DEVICE
        history.go(-1);
    }
});

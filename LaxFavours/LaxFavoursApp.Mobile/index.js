$(function() {
    var startupView = "About";

    // Uncomment the line below to disable platform-specific look and feel and to use the Generic theme for all devices
    // DevExpress.devices.current({ platform: "generic" });

    if(DevExpress.devices.real().platform === "win") {
        $("body").css("background-color", "#000");
    }

    var isDeviceReady = false,
        isViewShown = false;

    function hideSplashScreen() {
        if(isDeviceReady && isViewShown) {
            navigator.splashscreen.hide();
        }
    }

	if(document.addEventListener) {
		document.addEventListener("deviceready", function () {
			isDeviceReady = true;
			hideSplashScreen();
			document.addEventListener("backbutton", function () {
				DevExpress.processHardwareBackButton();
			}, false);
		}, false);
	}

    function onViewShown() {
        isViewShown = true;
        hideSplashScreen();
        LaxFavoursApp.app.off("viewShown", onViewShown);
    }

    function onNavigatingBack(e) {
        if(e.isHardwareButton && !LaxFavoursApp.app.canBack()) {
            e.cancel = true;
            exitApp();
        }
    }

    function exitApp() {
        switch (DevExpress.devices.real().platform) {
            case "android":
                navigator.app.exitApp();
                break;
            case "win":
                MSApp.terminateApp('');
                break;
        }
    }

    var layoutSet = DevExpress.framework.html.layoutSets[LaxFavoursApp.config.layoutSet],
        navigation = LaxFavoursApp.config.navigation;


    LaxFavoursApp.app = new DevExpress.framework.html.HtmlApplication({
        namespace: LaxFavoursApp,
        layoutSet: layoutSet,
        animationSet: DevExpress.framework.html.animationSets[LaxFavoursApp.config.animationSet],
        navigation: navigation,
        commandMapping: LaxFavoursApp.config.commandMapping,
        navigateToRootViewMode: "keepHistory",
        useViewTitleAsBackText: true
    });

    $(window).on("unload", function() {
        LaxFavoursApp.app.saveState();
    });

    LaxFavoursApp.app.router.register(":view/:id", { view: startupView, id: undefined });
    LaxFavoursApp.app.on("viewShown", onViewShown);
    LaxFavoursApp.app.on("navigatingBack", onNavigatingBack);
    LaxFavoursApp.app.navigate();
});
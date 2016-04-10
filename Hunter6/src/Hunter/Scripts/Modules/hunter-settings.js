var HunterSettings = (function () {
    var settings = {"AlertDismissTimeoutInMilliseconds":5000};
    var result = {};

    for (var settingName in settings) {
        Object.defineProperty(result, settingName, {
            value: settings[settingName]
        });
    }

    return result;
})();
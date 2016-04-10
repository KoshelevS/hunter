var HunterSettings = (function () {
    var settings = <%= SettingsJSON %>;
    var result = {};

    for (var settingName in settings) {
        Object.defineProperty(result, settingName, {
            value: settings[settingName]
        });
    }

    return result;
})();
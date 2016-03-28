var HunterSettings = (function () {
    'use strict';

    var settings = {
        'alertDismissTimeout' : 5000
    };

    return {
        getAlertDismissTimeout: function () {
            return settings.alertDismissTimeout;
        }
    };
})();
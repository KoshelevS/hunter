var HunterSettings = (function () {
    'use strict';

    return {
        get alertDismissTimeout () {
            return <%= AlertDismissTimeoutInMilliseconds %>;
        }
    };
})();
(function () {
    angular.module('hunterCommonServices', [])
        .factory('alertService', function () {
            var alerts = [];

            return {
                setScope: function (scope) {
                    scope.alerts = alerts;
                },
                addSuccessAlert: function (msg) {
                    alerts.push({
                        type: 'success',
                        message: msg
                    });
                },
                addInfoAlert: function (msg) {
                    alerts.push({
                        type: 'info',
                        message: msg
                    });
                },
                addWarningAlert: function (msg) {
                    alerts.push({
                        type: 'warning',
                        message: msg
                    });
                },
                addDangerAlert: function (msg) {
                    alerts.push({
                        type: 'danger',
                        message: msg
                    });
                },
                removeAlert: function (index) {
                    alerts.splice(index, 1);
                }
            };
        });
})();
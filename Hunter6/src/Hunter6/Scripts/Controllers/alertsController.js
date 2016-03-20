(function () {
    'use strict';

    function alertsController($scope) {
        HunterAlerts.setScope($scope);

        $scope.alertDismissTimeout = HunterSettings.getAlertDismissTimeout();

        $scope.closeAlert = function (index) {
            HunterAlerts.removeAlert(index)
        };
    }

    angular
        .module('projectsApp')
        .controller('alertsController', alertsController)

    alertsController.$inject = ['$scope'];
})();
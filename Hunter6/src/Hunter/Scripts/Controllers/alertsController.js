(function () {

    function alertsController($scope) {
        HunterAlerts.setScope($scope);

        $scope.alertDismissTimeout = HunterSettings.AlertDismissTimeoutInMilliseconds;

        $scope.closeAlert = function (index) {
            HunterAlerts.removeAlert(index);
        };
    }

    angular
        .module('app')
        .controller('alertsController', alertsController);

    alertsController.$inject = ['$scope'];
})();
(function () {

    function alertsController($scope, alertService, settingService) {
        alertService.setScope($scope);

        $scope.alertDismissTimeout = settingService.AlertDismissTimeoutInMilliseconds;

        $scope.closeAlert = function (index) {
            alertService.removeAlert(index);
        };
    }

    angular
        .module('app')
        .controller('alertsController', alertsController);

    alertsController.$inject = ['$scope', 'alertService', 'settingService'];
})();
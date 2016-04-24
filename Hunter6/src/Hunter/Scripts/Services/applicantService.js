(function () {

    var applicantService = angular.module('applicantService', ['ngResource']);
    applicantService.factory('Applicant', ['$resource',
        function ($resource) {
            return $resource('/api/applicant', {}, {
                query: { method: 'Get', params: {}, isArray: true }
            });
        }
    ]);
})();
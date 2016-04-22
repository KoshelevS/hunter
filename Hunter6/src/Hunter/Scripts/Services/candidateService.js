(function () {

    var candidateService = angular.module('candidateService', ['ngResource']);
    candidateService.factory('Candidate', ['$resource',
        function ($resource) {
            return $resource('/api/applicant', {}, {
                query: { method: 'Get', params: {}, isArray: true }
            });
        }
    ]);
})();
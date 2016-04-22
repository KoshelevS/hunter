(function () {

    var candidateService = angular.module('candidateService', ['ngResource']);
    candidateService.factory('Candidate', ['$resource',
        function ($resource) {
            return $resource('/api/candidate', {}, {
                query: { method: 'Get', params: {}, isArray: true }
            });
        }
    ]);
})();
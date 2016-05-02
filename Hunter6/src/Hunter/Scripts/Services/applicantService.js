(function() {

    var applicantModule = angular.module('applicantModule', ['ngResource']);

    applicantModule.service('applicantService', [
        '$resource',
        function($resource) {
            var url = '/api/Applicant/:id';
            var paramDefaults = {};
            var actions = {
                query: { method: 'GET', params: { id: '' }, isArray: true },
                get: { method: 'GET', params: { id: '@id' } },
                remove: { method: 'DELETE', params: { id: '@id' } },
                update: { method:'PUT' }
            }
            return $resource(url, paramDefaults, actions);
        }
    ]);
})();
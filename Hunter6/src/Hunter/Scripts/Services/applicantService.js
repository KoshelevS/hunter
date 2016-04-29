(function () {

    var applicantModule = angular.module('applicantModule', ['ngResource']);

    applicantModule.factory('applicantService', ['$resource',
        function ($resource) {
            return $resource('/api/applicant/:id', {}, {
                query: { method: 'GET', params: { id: '' }, isArray: true },
                create: { method: 'POST' },
                get: { method: 'GET' },
                update: { method: 'PUT' },
                remove: { method: 'DELETE' }
            });
        }
    ]);
})();
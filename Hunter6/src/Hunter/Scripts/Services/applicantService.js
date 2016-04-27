(function () {

    var applicantModule = angular.module('applicantModule', ['ngResource']);

    applicantModule.factory('applicantService', ['$resource',
        function ($resource) {
            return $resource('/api/applicant/:id', {}, {
                query: { method: 'Get', params: {id:''}, isArray: true },
                remove: { method: 'DELETE' }
            });
        }
    ]);
})();
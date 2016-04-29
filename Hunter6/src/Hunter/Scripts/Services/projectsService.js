(function () {

    var projectsService = angular.module('projectsService', ['ngResource']);
    projectsService.factory('Projects', ['$resource',
        function ($resource) {
            return $resource('/api/project/:id', {}, {
                query: { method: 'GET', params: {}, isArray: true },
                create: { method: 'POST' },
                get: { method: 'GET' },
                update: { method: 'PUT' },
                remove: { method: 'DELETE' }
            });
        }
    ]);
})();
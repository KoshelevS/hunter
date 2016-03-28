(function () {
    'use strict';

    var projectsService = angular.module('projectsService', ['ngResource']);
    projectsService.factory('Projects', ['$resource',
        function ($resource) {
            return $resource('/api/project', {}, {
                query: { method: 'Get', params: {}, isArray: true }
            });
        }
    ]);
})();
(function () {
    'use strict';

    var projectsService = angular.module('projectsService', ['ngResource']);
    projectsService.factory('Projects', ['$resource',
        function ($resource) {
//            return $resource('/api/project', {}, {
            return $resource('/api/ProjectsNew', {}, {
                query: { method: 'GET', params: {}, isArray: true }
            });
        }
    ]);
})();
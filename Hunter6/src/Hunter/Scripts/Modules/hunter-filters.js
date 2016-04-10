(function () {

    angular
      .module('hunterFilters', [])
        .filter('fileSize', ['$filter', function ($filter) {
            return function (input, fractionSize) {
                if (input > 1024 * 1024)
                    return $filter('number')(input / 1024 / 1024, fractionSize) + ' MB';
            
                if (input > 1024)
                    return $filter('number')(input / 1024, fractionSize) + ' KB';

                return input + ' B';
            };
        }]);
})();
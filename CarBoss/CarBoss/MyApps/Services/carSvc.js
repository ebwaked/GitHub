angular.module('carBossApp')
    .factory('carSvc', ['$http', function ($http) {
        var factory = {};

        factory.getYears = function () {
            return $http.get('/api/cars/getYears')
                .then(function (response) { return response.data});
        };

        factory.getMakes = function (year) {
            var options = { params: { year: year } };

            return $http.get('/api/cars/getMakes')
                .then(function (response) { return response.data });
        };

        factory.getMakes = function (year, make) {
            var options = { params: { year: year, make: make } };

            return $http.get('/api/cars/getMakes')
                .then(function (response) { return response.data });
        };

        return factory;
    }]);

angular.module('carBossApp')
    .controller('CarsController', ['carSvc', function (carSvc) {
        var scope = this;
        scope.selectedYear = '';
        scopre.years = [];

        scope.getYears = function () {
            carSvc.getYears().then(function (data) { scope.years = data; })
        };
        // To get it going
        scope.getYears();
    }]);
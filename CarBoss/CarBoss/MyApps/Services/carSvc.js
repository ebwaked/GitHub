angular.module('carBossApp')
    .factory('carSvc', ['$http', function ($http) {
        var factory = {};

        factory.getYears = function () {
            return $http.get('/api/cars/getYears')
                .then(function (response) { return response.data});
        };

        factory.getMakes = function (year) {
            var options = { params: { year: year } };

            return $http.get('/api/cars/getMakes', options)
                .then(function (response) { return response.data });
        };

        factory.getModels = function (year, make) {
            var options = { params: { year: year, make: make } };

            return $http.get('/api/cars/getModels', options)
                .then(function (response) { return response.data });
        };

        factory.getTrims = function (year, make, model) {
            var options = { params: { year: year, make: make, model: model } };

            return $http.get('/api/cars/getTrims', options)
                .then(function (response) { return response.data });
        };

        factory.getCars = function (year, make, model, trim) {
            var options = { params: { year: year, make: make, model: model, trim: trim } };

            return $http.get('/api/cars/getCars', options)
                .then(function (response) { return response.data });
        };

        factory.getRecalls = function (year, make, model) {
            var options = { params: { year: year, make: make, model: model } }
            return $http.get('/api/cars/getRecalls', options), then(function (response) { return response.data });
        }
        return factory;
    }]);

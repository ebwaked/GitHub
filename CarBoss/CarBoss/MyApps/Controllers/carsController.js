angular.module('carBossApp')
    .controller('CarsController', ['carSvc', function (carSvc) {
        var scope = this;
        scope.selectedYear = '';
        scope.years = [];
        scope.selectedMake = '';
        scope.makes = [];
        scope.selectedModel = '';
        scope.models = [];
        scope.selectedTrim = '';
        scope.trims = [];


        scope.getYears = function () {
            carSvc.getYears().then(function (data) { scope.years = data; })
        };
        // To get it going
        scope.getYears();

        scope.getMakes = function () {
            scope.selectedMake = '';
            scope.makes = [];
            scope.selectedModel = '';
            scope.models = [];
            scope.selectedTrim = '';
            scope.trims = [];

            carSvc.getMakes(scope.selectedYear).then(function (data) { scope.makes = data; scope.getCars()})
        };

        scope.getModels = function () {
            scope.selectedModel = '';
            scope.models = [];
            scope.selectedTrim = '';
            scope.trims = [];

            carSvc.getModels(scope.selectedYear, scope.selectedMake).then(function (data) { scope.models = data; scope.getCars()})
        };

        scope.getTrims = function () {
            scope.selectedTrim = '';
            scope.trims = [];

            carSvc.getTrims(scope.selectedYear, scope.selectedMake, scope.selectedModel).then(function (data) { scope.trims = data; scope.getCars()})
        };

        scope.getCars = function () {
            carSvc.getCars(scope.selectedYear, scope.selectedMake, scope.selectedModel, scope.selectedTrim).then(function (data) { scope.cars = data; })
        };
    }]);
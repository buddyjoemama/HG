(function () {
    'use strict';

    angular
        .module('humanGavelUser.app')
        .component('hgCaseList',
        {
            templateUrl: 'Components/CaseList/hgCaseList.html',
            controller: function (casesService) {
                var $ctrl = this;
                $ctrl.cases = casesService.getSampleData();
            }
        });
})();

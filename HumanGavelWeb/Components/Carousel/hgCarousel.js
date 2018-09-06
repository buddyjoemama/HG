(function () {
    'use strict';

    angular
        .module('humanGavelUser.app')
        .component('hgCarousel',
        {
            templateUrl: 'Components/Carousel/carousel.html',
            controller: function (casesService) {
                var $ctrl = this;

                $ctrl.slides = casesService.getSampleData();
            }
        });
})();
  
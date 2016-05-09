(function ($, angular) {
    (function () {
        $('#deliveryIndextTab a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        });
    })();

    angular.module('mvcapp', ['ui.bootstrap', 'ngMessages', 'ServiceCommon'])
        .controller('DeliveryIndexCtrl', ['$scope', '$http', '$q', function () {

        }]);
})(jQuery, angular)
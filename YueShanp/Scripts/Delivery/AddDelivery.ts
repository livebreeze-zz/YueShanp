class DeliveryOrder {
    CustomerId: number;
    OrderNumber: number;
    DeliveryDate: string;
    AccountMonth: string;
    ProductList: Array<Product>; 
}

class Product {
    ProductId: number;
    ProductName: string;
    UnitPrice: number;
    Qty: number;
}

(function ($, angular) {
    angular.module('mvcapp', ['ui.bootstrap', 'ngMessages', 'ServiceCommon', 'CommonHelper'])
        .controller('addDeliveryCtrl', ['$scope', '$http', '$filter', '$q',
            function ($scope, $http, $filter, $q) {
                // MODELs
                $scope.selectCustomer = '';
                $scope.deliveryOrderNumber = 0;
                $scope.deliveryDate = $scope.FormatDate(new Date());
                $scope.accountMonth = '';

                // ATTRIBUTEs
                $scope.format = 'yyyy/MM/dd';

                // FUNCTIONs
                $scope.FormatDate = function (date) {
                    return $filter('date')(date, $scope.format);
                };

            }]);
})(jQuery, angular)
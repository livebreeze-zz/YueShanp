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

(function () {
    angular.module('mvcApp', ['ui.bootstrap', 'ServiceCommon', 'CommonHelper'])
        .controller('addDeliveryCtrl', ['$scope', '$http', '$filter',
            function ($scope, $http, $filter) {
                // FUNCTIONs
                $scope.FormatDate = function (date) {
                    return $filter('date')(date, $scope.format);
                };

                // ATTRIBUTEs
                $scope.format = 'yyyy/MM/dd';

                // MODELs
                $scope.selectCustomer = '';
                $scope.deliveryOrderNumber = 0;
                $scope.deliveryDate = $scope.FormatDate(new Date());
                $scope.accountMonth = '';
            }]);
})();
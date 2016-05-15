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
                $scope.AddProduct = function () {
                    $scope.ProductList.push(new Product());
                };
                $scope.getOrderTotalAmount = function () {
                    let result = 0;
                    $scope.ProductList.forEach(function (entry: Product) {
                        result += entry.UnitPrice * entry.Qty;
                    });

                    return result;
                };

                $scope.RemoveProduct = function (index) {
                    $scope.ProductList.splice(index, 1);
                };

                // ATTRIBUTEs
                $scope.format = 'yyyy/MM/dd';

                // MODELs
                $scope.selectCustomer = '';
                $scope.deliveryOrderNumber;
                $scope.deliveryDate = $scope.FormatDate(new Date());
                $scope.accountMonth = '';
                $scope.ProductList = new Array<Product>();
            }]);
})();   
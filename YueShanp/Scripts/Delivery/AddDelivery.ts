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
        .controller('addDeliveryCtrl', ['$scope', '$http', '$filter', 'YSService',
            function ($scope, $http, $filter, ysService: IYSService) {
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
                $scope.SavePrint = function () {
                    // TODO 將資料  POST 到 SERVICE
                    var request = <ng.IRequestShortcutConfig>{
                        data: {
                            
                        }
                    };
                    debugger;
                    ysService.PostDeliveryOrder(request);

                    // TODO 修改 MODAL 由 SERVICE 取值，或者先將資料記載臨時資料區
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
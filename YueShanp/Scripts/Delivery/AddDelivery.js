//class DeliveryOrder {
//    CustomerId: number;
//    OrderNumber: number;
//    DeliveryDate: string;
//    AccountMonth: string;
//    ProductList: Array<Product>; 
//}
(function () {
    angular.module('mvcApp', ['ui.bootstrap', 'ServiceCommon', 'CommonHelper'])
        .controller('addDeliveryCtrl', ['$scope', '$filter', 'YSService', '$window',
        function ($scope, $filter, ysService, $window) {
            // FUNCTIONs
            $scope.FormatDate = function (date) {
                return $filter('date')(date, $scope.format);
            };
            $scope.AddProduct = function () {
                $scope.DeliveryOrderDetailList.push(new DeliveryOrderDetail(0, new Product(0, '', 0)));
            };
            // TODO this has bug, why I can't get the damm product's unit price
            $scope.getOrderTotalAmount = function () {
                var result = 0;
                $scope.DeliveryOrderDetailList.forEach(function (entry) {
                    result += entry.Product.UnitPrice * entry.Qty;
                });
                return result;
            };
            $scope.SavePrint = function () {
                var customerId = $scope.customerId;
                var customerSONumber = $scope.CustomerSONumber;
                var deliveryOrderDate = $scope.deliveryDate;
                var deliveryOrderNumber = $scope.deliveryOrderNumber;
                var receivableMonth = $scope.accountMonth;
                var deliveryOrderDetailList = $scope.DeliveryOrderDetailList;
                var config = {
                    data: {
                        Customer: new Customer(customerId),
                        CustomerSONumber: customerSONumber,
                        DeliveryOrderDate: deliveryOrderDate,
                        DeliveryOrderNumber: deliveryOrderNumber,
                        ReceivableMonth: receivableMonth,
                        DeliveryOrderDetailList: deliveryOrderDetailList
                    }
                };
                debugger;
                ysService.PostDeliveryOrder(config);
                $scope.isSaved = true;
                // TODO 修改 MODAL 由 SERVICE 取值，或者先將資料記載臨時資料區
            };
            $scope.RemoveProduct = function (index) {
                $scope.DeliveryOrderDetailList.splice(index, 1);
            };
            // ATTRIBUTEs
            $scope.format = 'yyyy/MM/dd';
            // MODELs
            $scope.selectCustomer = '';
            $scope.deliveryOrderNumber;
            $scope.deliveryDate = $scope.FormatDate(new Date());
            $scope.accountMonth = '';
            $scope.DeliveryOrderDetailList = new Array();
            // TODO remove this in DeliveryOrderDetail
            //$scope.ProductList = new Array<Product>();
            $scope.customerId = '';
            ////Show warning message if user leave page ---------------------------------------------------------------////
            $scope.$watch('drform.$dirty', function (value) {
                if (value && !$scope.isSaved) {
                    $window.onbeforeunload = function () {
                        if (!$scope.isSaved) {
                            return "Your data will be lost, if you're leave!! Do you want to leave?";
                        }
                    };
                }
            });
        }]);
})();
//# sourceMappingURL=AddDelivery.js.map
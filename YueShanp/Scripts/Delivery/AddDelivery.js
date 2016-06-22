(function () {
    var CustomerOption = (function () {
        function CustomerOption() {
        }
        return CustomerOption;
    })();
    angular.module('mvcApp', ['ui.bootstrap', 'ServiceCommon', 'CommonHelper'])
        .factory('addDeliveryFactory', function () {
        function GetCustomerOptions(customerList) {
            var options = new Array();
            options.push({ value: '0', name: '選擇廠商' });
            angular.forEach(customerList, function (value, key) {
                options.push({ value: value.Id.toString(), name: value.Name });
            });
            return options;
        }
        return {
            GetCustomerOptions: GetCustomerOptions
        };
    })
        .controller('addDeliveryCtrl', ['$scope', '$filter', 'YSService', '$window', 'addDeliveryFactory',
        function ($scope, $filter, ysService, $window, addDeliveryFactory) {
            var now = new Date();
            // FUNCTIONs
            $scope.PrePrintClick = function ($event) {
                var thisButton = angular.element($event.target);
                thisButton.button('loading');
                var callback = function () {
                    thisButton.button('reset');
                    var prePrintDeliveryOrderUrl = '/Delivery/PrePrintDeliveryOrder?DONumber=' + $scope.deliveryOrderNumber;
                    $window.open(prePrintDeliveryOrderUrl, 'DeliveryPrint', 'height=800,width=800');
                };
                $scope.SavePrint(callback);
            };
            $scope.FormatDate = function (date) {
                return $filter('date')(date, $scope.format);
            };
            $scope.AddProduct = function () {
                angular.forEach($scope.productSelected, function (selectedId) {
                    angular.forEach($scope.productList, function (product) {
                        if (selectedId == product.Id) {
                            // 防止重複加入
                            var dodList = $scope.deliveryOrderDetailList;
                            if (!dodList.some(function (elem) { return elem.Product.Id == selectedId; })) {
                                $scope.deliveryOrderDetailList.push(new DeliveryOrderDetail(0, product));
                            }
                            return;
                        }
                    });
                });
            };
            $scope.getOrderTotalAmount = function () {
                var result = 0;
                $scope.deliveryOrderDetailList.forEach(function (entry) {
                    result += entry.Product.UnitPrice * entry.Qty;
                });
                return result;
            };
            $scope.SavePrint = function (callback) {
                var customerSONumber = $scope.CustomerSONumber;
                var deliveryOrderDate = $scope.deliveryDate;
                var deliveryOrderNumber = $scope.deliveryOrderNumber;
                var receivableMonth = $scope.accountMonth;
                var deliveryOrderDetailList = $scope.deliveryOrderDetailList;
                var config = {
                    data: {
                        Customer: new Customer($scope.selectedCustomer),
                        CustomerSONumber: customerSONumber,
                        DeliveryOrderDate: deliveryOrderDate,
                        DeliveryOrderNumber: deliveryOrderNumber,
                        ReceivableMonth: receivableMonth,
                        DeliveryOrderDetailList: deliveryOrderDetailList
                    }
                };
                ysService.PostDeliveryOrder(config).then(function () {
                    $scope.isSaved = true;
                    callback();
                });
            };
            $scope.RemoveProduct = function (index) {
                $scope.deliveryOrderDetailList.splice(index, 1);
            };
            $scope.ResetProductList = function () {
                ysService.GetProductList($scope.selectedCustomer || 0).then(function (data) {
                    $scope.productList = data;
                });
            };
            // ATTRIBUTEs
            $scope.format = 'yyyy/MM/dd';
            // MODELs
            //$scope.selectedCustomer = 0;
            $scope.deliveryOrderNumber;
            $scope.deliveryDate = $scope.FormatDate(now);
            $scope.accountMonth = (now.getFullYear() - 1911).toString() + ('0' + (now.getMonth() + 1).toString()).slice(-2);
            $scope.deliveryOrderDetailList = new Array();
            $scope.customerList = ysService.GetCustomerList().then(function (data) {
                $scope.customerOptions = addDeliveryFactory.GetCustomerOptions(data);
                $scope.selectedCustomer = $scope.customerOptions[0].value;
            });
            $scope.productList = new Array();
            $scope.productSelected = '';
            ////Show warning message if user leave page ---------------------------------------------------------------////
            $scope.$watch('addDeliveryform.$dirty', function (value) {
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
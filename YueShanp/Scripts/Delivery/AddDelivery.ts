(function () {
    class CustomerOption {
        value: string;
        name: string;
    }

    interface IAddDeliveryFactory {
        GetCustomerOptions(customerList: Customer[])
    }

    angular.module('mvcApp', ['ui.bootstrap', 'ServiceCommon', 'CommonHelper'])

        .factory('addDeliveryFactory', function () {

            function GetCustomerOptions(customerList: Customer[]) {
                var options = new Array<CustomerOption>();
                options.push({ value: '0', name: '選擇廠商' });
                angular.forEach(customerList, function (value: Customer, key) {
                    options.push({ value: value.Id.toString(), name: value.Name });
                });
                return options;
            }

            return <IAddDeliveryFactory>{
                GetCustomerOptions
            }
        })


        .controller('addDeliveryCtrl', ['$scope', '$filter', 'YSService', '$window', 'addDeliveryFactory',
            function ($scope, $filter, ysService: IYSService, $window: ng.IWindowService, addDeliveryFactory: IAddDeliveryFactory) {
                var now: Date = new Date();

                // FUNCTIONs
                $scope.PrePrintClick = function () {
                    let prePrintDeliveryOrderUrl = '/Delivery/PrePrintDeliveryOrder?DONumber=' + $scope.deliveryOrderNumber;
                    $window.open(prePrintDeliveryOrderUrl, 'DeliveryPrint', 'height=800,width=800');
                }
                $scope.FormatDate = function (date) {
                    return $filter('date')(date, $scope.format);
                };
                $scope.AddProduct = function () {
                    angular.forEach($scope.productSelected, function (selectedId: number) {
                        angular.forEach($scope.productList, function (product: Product) {
                            if (selectedId == product.Id) {
                                // 防止重複加入
                                var dodList: Array<DeliveryOrderDetail> = $scope.deliveryOrderDetailList;
                                if (!dodList.some(elem => elem.Product.Id == selectedId)) {
                                    $scope.deliveryOrderDetailList.push(new DeliveryOrderDetail(0, product));
                                }

                                return;
                            }
                        });
                    });
                };

                $scope.getOrderTotalAmount = function () {
                    let result = 0;
                    $scope.deliveryOrderDetailList.forEach(function (entry: DeliveryOrderDetail) {
                        result += entry.Product.UnitPrice * entry.Qty;
                    });

                    return result;
                };

                $scope.SavePrint = function () {
                    //var customerId = $scope.customerId;
                    var customerSONumber = $scope.CustomerSONumber;
                    var deliveryOrderDate = $scope.deliveryDate;
                    var deliveryOrderNumber = $scope.deliveryOrderNumber;
                    var receivableMonth = $scope.accountMonth;
                    var deliveryOrderDetailList = $scope.deliveryOrderDetailList;

                    var config = <IDeliveryOrderConfig>{
                        data: {
                            Customer: new Customer($scope.selectedCustomer),
                            CustomerSONumber: customerSONumber,
                            DeliveryOrderDate: deliveryOrderDate,
                            DeliveryOrderNumber: deliveryOrderNumber,
                            ReceivableMonth: receivableMonth,
                            DeliveryOrderDetailList: deliveryOrderDetailList
                        }
                    };
                    ysService.PostDeliveryOrder(config);
                    $scope.isSaved = true;

                    // TODO 修改 MODAL 由 SERVICE 取值，或者先將資料記載臨時資料區
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
                $scope.deliveryOrderDetailList = new Array<DeliveryOrderDetail>();
                $scope.customerList = ysService.GetCustomerList().then(
                    function (data: Array<Customer>) {
                        $scope.customerOptions = addDeliveryFactory.GetCustomerOptions(data);
                        $scope.selectedCustomer = $scope.customerOptions[0].value;
                    });

                $scope.productList = new Array<Product>();
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
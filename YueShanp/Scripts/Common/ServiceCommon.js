var Customer = (function () {
    function Customer(id) {
        this.Id = id;
    }
    return Customer;
})();
var DeliveryOrderDetail = (function () {
    function DeliveryOrderDetail(qty, product) {
        this.Qty = qty;
        this.Product = product;
    }
    return DeliveryOrderDetail;
})();
var Product = (function () {
    function Product() {
    }
    return Product;
})();
(function (angular) {
    angular.module('ServiceCommon', [])
        .constant('YSConfig', {
        HostUrl: 'http://localhost:11074/'
    })
        .factory('YSService', ['$http', '$q', 'YSConfig',
        function ($http, $q, YSConfig) {
            var hostUrl = YSConfig.HostUrl;
            var addDeliveryOrderUrl = hostUrl + "/ajax/AddDeliveryOrder";
            function PostDeliveryOrder(config) {
                debugger;
                var deferred = $q.defer();
                var opts = {
                    url: addDeliveryOrderUrl,
                    method: 'POST'
                };
                angular.extend(opts, config);
                $http(opts).then(function (response) {
                    debugger;
                    var responseObj = response.data;
                    if (responseObj) {
                        if (responseObj.IsSuccess) {
                            deferred.resolve(responseObj);
                        }
                    }
                    deferred.reject('Data not found.');
                }, function (response) {
                    deferred.reject(response);
                });
                return deferred.promise;
            }
            ;
            return {
                PostDeliveryOrder: PostDeliveryOrder
            };
        }]);
})(angular);
//# sourceMappingURL=ServiceCommon.js.map
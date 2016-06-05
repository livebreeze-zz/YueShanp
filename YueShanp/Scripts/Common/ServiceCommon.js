(function (angular) {
    angular.module('ServiceCommon', [])
        .constant('YSConfig', {
        HostUrl: 'http://localhost:11074/'
    })
        .factory('YSService', ['$http', '$q', 'YSConfig',
        function ($http, $q, YSConfig) {
            var hostUrl = YSConfig.HostUrl;
            var addDeliveryOrderUrl = hostUrl + "/ajax/AddDeliveryOrder";
            function PostDeliveryOrder(request) {
                debugger;
                var deferred = $q.defer();
                var opts = {
                    url: addDeliveryOrderUrl,
                    method: 'POST'
                };
                angular.extend(opts, request);
                $http(opts).then(function (response) {
                    debugger;
                    var responseObj = response.data;
                    if (responseObj) {
                        if (responseObj.IsSuccess) {
                            //// TODO: 設定頁面需要用到的接口
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

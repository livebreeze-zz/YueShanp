interface IYSService {
    PostDeliveryOrder(request: IDeliveryOrderRequest): ng.IPromise<{}>;
}

interface IYSConfig {
    HostUrl: string,
}

interface IRespResult {
    IsSuccess: boolean,
    Data: any,
    ErrMsg: string
}

interface IDeliveryOrderRequest {
    // TODO set request model.
}

(function (angular) {
    angular.module('ServiceCommon', [])
        .constant('YueShanpConfig', <IYSConfig>{
            HostUrl: 'http://localhost:11074/'
        })

        .factory('YSService', ['$http', '$q', 'YSConfig',
            function ($http: ng.IHttpService, $q: ng.IQService, YSConfig: IYSConfig) {
                let hostUrl = YSConfig.HostUrl;
                let addDeliveryOrderUrl = `${hostUrl}/ajax/addDeliveryOrderUrl`;

                function PostDeliveryOrder(request: IDeliveryOrderRequest) {
                    let deferred = $q.defer();
                    let opts = <ng.IRequestConfig>{
                        url: addDeliveryOrderUrl,
                        method: 'POST'
                    };

                    angular.extend(opts, request);

                    $http(opts).then(function (response) {
                        let responseObj = <IRespResult>response.data;
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
                };

                return <IYSService>{
                    PostDeliveryOrder
                }
        }]);

})(angular)
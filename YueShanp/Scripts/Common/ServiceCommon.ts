interface IYSService {
    PostDeliveryOrder(config: IDeliveryOrderConfig): ng.IPromise<any>;
    GetCustomerList(): ng.IPromise<any>;
    GetProductList(customerId: number): ng.IPromise<any>;
}

interface IYSConfig {
    HostUrl: string;
}

interface IRespResult {
    IsSuccess: boolean,
    Data: any,
    ErrorMessage: string;
}

interface IGetCustomerListRespResult extends IRespResult {
    Customers: Array<Customer>
}

interface IGetProductListRespResult extends IRespResult {
    Products: Array<Product>
}

interface IDeliveryOrderRequest {
    DeliveryOrderNumber: number,
    CustomerSONumber: string,
    DeliveryOrderDate: string,
    ReceivableMonth: string,
    Customer: Customer,
    DeliveryOrderDetailList: Array<DeliveryOrderDetail>;
}

interface IDeliveryOrderConfig extends ng.IRequestShortcutConfig {
    data: IDeliveryOrderRequest;
}

class Customer {
    Id: number;
    Name: string;
    FullName: string;
    Phone: string;
    Fax: string;
    Address: string;
    Email: string;
    Purchaser: string;
    TIN: string;

    constructor(id: number) {
        this.Id = id;
    }
}

class DeliveryOrderDetail {
    Qty: number;
    Product: Product

    constructor(qty: number, product: Product) {
        this.Qty = qty;
        this.Product = product;
    }
}

class Product {
    Id: number;
    ProductName: string;
    UnitPrice: number;

    constructor(id: number, productName: string, unitPrice: number) {
        this.Id = id;
        this.ProductName = productName;
        this.UnitPrice = unitPrice;
    }
}

(function (angular) {
    angular.module('ServiceCommon', [])
        .constant('YSConfig', <IYSConfig>{
            HostUrl: 'http://localhost:11074'
            //HostUrl: 'http://59.126.180.155'
        })

        .factory('YSService', ['$http', '$q', 'YSConfig',
            function ($http: ng.IHttpService, $q: ng.IQService, YSConfig: IYSConfig) {
                var hostUrl: string = YSConfig.HostUrl;
                var addDeliveryOrderUrl: string = `${hostUrl}/ajax/AddDeliveryOrder`;
                var getCustomerListUrl: string = `${hostUrl}/ajax/GetCustomerList`;
                var getProductListUrl: string = `${hostUrl}/ajax/GetProductList`;

                function PostDeliveryOrder(config: IDeliveryOrderConfig) {
                    var deferred = $q.defer();
                    var opts = <ng.IRequestConfig>{
                        url: addDeliveryOrderUrl,
                        method: 'POST'
                    };

                    angular.extend(opts, config);

                    $http(opts).then(function (response) {
                        debugger;
                        var responseObj = <IRespResult>response.data;
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
                };

                function GetCustomerList() {
                    var deferred = $q.defer();
                    var opts = <ng.IRequestConfig>{
                        url: getCustomerListUrl,
                        method: 'GET'
                    };

                    $http(opts).then(function (response) {
                        var obj = <IGetCustomerListRespResult>response.data;
                        var customerList: Array<Customer> = [];

                        if (obj && obj.IsSuccess) {
                            angular.forEach(obj.Customers, function (value: Customer, key) {
                                customerList.push(value);
                            });                            

                            deferred.resolve(customerList);
                        } else {
                            deferred.reject('Data not found.');
                        }
                    }, function (response) {
                        deferred.reject(response);
                    });

                    return deferred.promise;
                }

                function GetProductList(customerId: number) {
                    var deferred = $q.defer();
                    var opts = <ng.IRequestConfig>{
                        url: getProductListUrl + '/' + customerId,
                        method: 'GET'
                    };

                    $http(opts).then(function (response) {
                        var obj = <IGetProductListRespResult>response.data;
                        var productList: Array<Product> = [];

                        if (obj && obj.IsSuccess) {
                            angular.forEach(obj.Products, function (value: Product, key) {
                                productList.push(value);
                            });                            

                            deferred.resolve(productList);
                        } else {
                            deferred.reject('Data not found.');
                        }
                    }, function (response) {
                        deferred.reject(response);
                    });

                    return deferred.promise;
                }

                return <IYSService>{
                    PostDeliveryOrder,
                    GetCustomerList,
                    GetProductList
                };
            }]);

})(angular);
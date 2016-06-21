(function () {
    angular.module('CommonHelper', [])
        .directive('drcalander', function () {
            var tpl = '<div class="input-group">' +
                '<input name="{{name}}" type="text" class="form-control" placeholder= "yyyy/MM/dd" uib-datepicker-popup="{{format}}"' +
                'ng-model="datemodel" ng-value="datemodel" is-open="popupDateCalander.opened" close-text="Close" ng-click="openDateCalander()" readonly required />' +
                '<span class="input-group-btn" >' +
                '<button type="button" class="btn btn-default" ng-click="openDateCalander()" ><i class="fa fa-calendar" ></i></button>' +
                '</span>' +
                '</div>';
            return {
                restrict: 'E',
                template: tpl,
                replace: true,
                scope: { format: '@', datemodel: '=', name: '@' },
                link: function (scope: any) {
                    scope.openDateCalander = function () {
                        scope.popupDateCalander.opened = true;
                    };

                    scope.popupDateCalander = {
                        opened: false
                    };
                }
            };
        });
})();
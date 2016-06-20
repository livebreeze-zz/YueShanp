jQuery(document).ready(function ($) {
    var modal = $('#modal-add-product');
    var alert = modal.find('.alert');
    modal.on('hidden.bs.modal', function () {
        $(this).empty();
        $(this).removeData('bs.modal');
    });
    if ($.YS.isEmpty(alert)) {
        alert.fadeOut();
    }
    modal.find('.alert');
    modal.find('.btn-submit').click(function () {
        var btn_submit = $(this);
        var _modal = btn_submit.parents('.modal');
        var _alert = _modal.find('.alert');
        var _form = _modal.find('form');
        _alert.empty().removeClass().addClass('alert').fadeOut();
        $.ajax({
            method: 'POST',
            url: '/ajax/QuickUpdateProduct',
            data: _form.serialize(),
            cache: false
        })
            .done(function (data) {
            if (data && data.IsSuccess) {
                _alert.addClass('alert-success').html('<strong>DONE!</strong> 新增/修改商品完成').fadeIn();
                setTimeout(function () {
                    // angular 刷新 productNameList
                    var $scope = angular.element('#addDeliveryBody').scope();
                    $scope.ResetProductList();
                    _modal.modal('hide');
                }, 1000);
            }
            else {
                _alert.addClass('alert-danger').html('<strong>ERROR!</strong> ' + data.ErrorMessage).fadeIn();
            }
        })
            .fail(function () {
            _alert.addClass('alert-danger').html('<strong>FAIL!</strong> 處理發生問題，請頁面重整後再試。').fadeIn();
        });
    });
});
//# sourceMappingURL=ModalAddProduct.js.map
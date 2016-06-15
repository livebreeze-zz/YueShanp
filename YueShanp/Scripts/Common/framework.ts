(function ($) {
    var YS = jQuery["YS"] = {};
    $.extend(YS, {
        isEmpty: function(el) {
            return !$.trim(el.html());
        }
    })
})(jQuery)
//初始化分组验证


function Validate(i) {
    var $group = $('.validationGroup'+i);

    var isValid = true;

    $group.find(':input').each(function(i, item) {
        if (!$(item).valid())
            isValid = false;
    });

    return isValid;
}
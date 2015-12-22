//远程验证抽象方法
function GetRemoteInfo(postUrl, data) {
    var remote = {
        type: "POST",
        async: false,
        url: postUrl,
        dataType: "xml",
        data: data,
        dataFilter: function (dataXML) {
            var result = new Object();
            result.Result = jQuery(dataXML).find("Result").text();
            result.Msg = jQuery(dataXML).find("Msg").text();
            if (result.Result == "-1") {
                result.Result = false;
                return result;
            }
            else {
                result.Result = result.Result == "1" ? true : false;
                return result;
            }
        }
    };
    return remote;
}


jQuery.validator.addMethod("stringCheck", function (value, element) {
    return this.optional(element) || /^[\u0391-\uFFE5\w]+$/.test(value);
}, "只能包括中文字、英文字母、数字和下划线");

//中文字两个字节       
jQuery.validator.addMethod("byteRangeLength", function (value, element, param) {
    var length = value.length;
    for (var i = 0; i < value.length; i++) {
        if (value.charCodeAt(i) > 127) {
            length++;
        }
    }
    return this.optional(element) || (length >= param[0] && length <= param[1]);
}, "请确保输入的值在3-15个字节之间(一个中文字算2个字节)");

//身份证号码验证       
jQuery.validator.addMethod("isIdCardNo", function (value, element) {
    return this.optional(element) || isIdCardNo(value);
}, "请正确输入您的身份证号码");

//手机号码验证       
jQuery.validator.addMethod("isMobile", function (value, element) {
    var length = value.length;
    //var mobile = /^(((13[0-9]{1})|(15[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    var mobile = /^[0-9]{11}$/;;
    return this.optional(element) || (length == 11 && mobile.test(value));
}, "请正确填写您的手机号码");

//电话号码验证       
jQuery.validator.addMethod("isTel", function (value, element) {
    var tel = /^\d{3,4}-?\d{7,9}$/;    //电话号码格式010-12345678   
    return this.optional(element) || (tel.test(value));
}, "请正确填写您的电话号码");

//联系电话(手机/电话皆可)验证   
jQuery.validator.addMethod("isPhone", function (value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    var tel = /^\d{3,4}-?\d{7,9}$/;
    return this.optional(element) || (tel.test(value) || mobile.test(value));

}, "请正确填写您的联系电话");

//邮政编码验证       
jQuery.validator.addMethod("isZipCode", function (value, element) {
    var tel = /^[0-9]{6}$/;
    return this.optional(element) || (tel.test(value));

}, "请正确填写您的邮政编码");

//价格
jQuery.validator.addMethod("isCurrency", function (value, element) {
    var tel = /^\d+(\.\d+)?$/;


    return this.optional(element) || (tel.test(value));

}, "请正确填写正确的价格");

//昵称
jQuery.validator.addMethod("isNickName", function (value, element) {
    var tel = /^[0-9]{2,20}$/;
    return this.optional(element) || !(tel.test(value));
}, "昵称仅可使用汉字,数字,字母和下划线,且不能为纯数字！");

//精确小数点之后一位
jQuery.validator.addMethod("isRadixPointOne", function (value, element) {
    var tel = /^\d+(\.\d)?$/; //   var tel1 = /^\d+(\.\d0)?$/;
    return this.optional(element) || (tel.test(value));

}, "请精确到小数点之后1位");



//时间对比验证
jQuery.validator.addMethod("compareDate", function (value, element, param) {
    var startDate = jQuery(param).val().replace(/\//g, "-").split("-");
    var endDate = value.replace(/\//g, "-").split("-");
    var date1 = new Date(startDate[0], startDate[1], startDate[2]);
    var date2 = new Date(endDate[0], endDate[1], endDate[2]);
    return date1 <= date2;

}, "开始时间不能晚于截止时间");



//整数对比验证
jQuery.validator.addMethod("compareInt", function (value, element, param) {
    var startInt = parseInt(jQuery(param).val(), 10);
    var endInt = parseInt(value, 10);
    return startInt <= endInt;
}, "开始整数不能大于截止整数");



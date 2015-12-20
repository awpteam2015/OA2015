
var pro = pro || {};
(function () {
    pro.UserInfo = pro.UserInfo || {};
    pro.UserInfo.HdPage = pro.UserInfo.HdPage || {};
    pro.UserInfo.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.UserInfo.HdPage.submit();
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var BindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(BindEntity[filedname]);
                }
                //行项目信息用json绑定控件
                alert(JSON.stringify(BindEntity.List));
            }
        },
        submit: function() {
            var postData = {};
            var head = pro.submitKit.getHeadJson();
            postData.Entity = head;
            postData.Command = "add";

            var msg = $.ajax({
                type: "POST",
                url: "/PermissionManager/UserInfo/Hd",
                data: postData,
                cache: false,
                async: false
            }).responseText;
            //  var obj = jQuery.pars
        },
        addTab: function (subtitle, url) {
        
        }
    };
})();



$(function () {
    pro.UserInfo.HdPage.initPage();
});
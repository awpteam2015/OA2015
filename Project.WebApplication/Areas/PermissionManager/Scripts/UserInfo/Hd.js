﻿
var pro = pro || {};
(function () {
    pro.UserInfo = pro.UserInfo || {};
    pro.UserInfo.HdPage = pro.UserInfo.HdPage || {};
    pro.UserInfo.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.UserInfo.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.UserInfo.HdPage.submit("Edit");
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && pro.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/PermissionManager/UserInfo/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    $.alertExtend.info();
                }
            ).fail(
             function (errordetails, errormessage) {
                 $.alertExtend.error();
             }
            );

        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {
                        UserCode: { required: true },
                        Password: { required: true },
                        UserName: { required: true }
                    },
                    messages: {
                        UserCode: { required: "站点代码必填!" },
                        Password: { required: "密码必填!" },
                        UserName: { required: "用户名必填!" }
                    },
                    errorPlacement: function (error, element) {
                        pro.commonKit.errorPlacementHd(error, element);
                    },
                    debug: false
                });
            },
            logicValidate: function () {
                return true;
            }
        },

        addTab: function (subtitle, url) {

        }

    };
})();



$(function () {
    pro.UserInfo.HdPage.initPage();
});
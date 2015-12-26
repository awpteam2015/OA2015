var pro = pro || {};
(function () {
    pro.UserFunctionDetail = pro.UserFunctionDetail || {};
    pro.UserFunctionDetail.HdPage = pro.UserFunctionDetail.HdPage || {};
    pro.UserFunctionDetail.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.UserFunctionDetail.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.UserFunctionDetail.HdPage.submit("Edit");
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
                url: "/PermissionManager/UserFunctionDetail/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    $.alertExtend.info();
                    parent.$("#btnSearch").trigger("click");
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
          PkId: { required: true  },
          UserCode: { required: true  },
          FunctionId: { required: true  },
          FunctionDetailId: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          UserCode:  "必填!",
          FunctionId:  "必填!",
          FunctionDetailId:  "必填!",
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
    pro.UserFunctionDetail.HdPage.initPage();
});



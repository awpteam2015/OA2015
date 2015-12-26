var pro = pro || {};
(function () {
    pro.Function = pro.Function || {};
    pro.Function.HdPage = pro.Function.HdPage || {};
    pro.Function.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Function.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Function.HdPage.submit("Edit");
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
                url: "/PermissionManager/Function/" + command,
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
          FunctionnName: { required: true  },
          ModuleId: { required: true  },
          FunctionUrl: { required: true  },
          Area: { required: true  },
          Controller: { required: true  },
          Action: { required: true  },
          IsDisplayOnMenu: { required: true  },
          RankId: { required: true  },
          Remark: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          FunctionnName:  "模块名称必填!",
          ModuleId:  "模块ID必填!",
          FunctionUrl:  "模块路径必填!",
          Area:  "必填!",
          Controller:  "必填!",
          Action:  "必填!",
          IsDisplayOnMenu:  "是否在菜单上显示1是 0不是必填!",
          RankId:  "顺序必填!",
          Remark:  "备注必填!",
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
    pro.Function.HdPage.initPage();
});



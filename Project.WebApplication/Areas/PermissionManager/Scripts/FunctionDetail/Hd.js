var pro = pro || {};
(function () {
    pro.FunctionDetail = pro.FunctionDetail || {};
    pro.FunctionDetail.HdPage = pro.FunctionDetail.HdPage || {};
    pro.FunctionDetail.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.FunctionDetail.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.FunctionDetail.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.FunctionDetail.ListPage.closeTab("");
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
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/PermissionManager/FunctionDetail/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.FunctionDetail.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
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
          FunctionDetailName: { required: true  },
          FunctionDetailCode: { required: true  },
          FunctionId: { required: true  },
          Area: { required: true  },
          Controller: { required: true  },
          Action: { required: true  },
          CreatorUserCode: { required: true  },
          CreationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          LastModificationTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          FunctionDetailName:  "功能名称必填!",
          FunctionDetailCode:  "功能代号对应页面需要控制的按钮Id必填!",
          FunctionId:  "模块ID必填!",
          Area:  "必填!",
          Controller:  "必填!",
          Action:  "必填!",
          CreatorUserCode:  "创建人必填!",
          CreationTime:  "创建时间必填!",
          LastModifierUserCode:  "修改人必填!",
          LastModificationTime:  "修改时间必填!",
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
    pro.FunctionDetail.HdPage.initPage();
});



var pro = pro || {};
(function () {
    pro.Module = pro.Module || {};
    pro.Module.HdPage = pro.Module.HdPage || {};
    pro.Module.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Module.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Module.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Module.ListPage.closeTab("");
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
                url: "/PermissionManager/Module/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Module.ListPage.closeTab();
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
        //  PkId: { required: true  },
          ModuleName: { required: true  }
          //ParentId: { required: true  },
          //ModuleLevel: { required: true  },
          //RankId: { required: true  },
          //Remark: { required: true  },
                    },
                    messages: {
          PkId:  "ID必填!",
          ModuleName:  "模块名称必填!",
          ParentId:  "父级 预留必填!",
          ModuleLevel:  "层级必填!",
          RankId:  "排序必填!",
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
    pro.Module.HdPage.initPage();
});



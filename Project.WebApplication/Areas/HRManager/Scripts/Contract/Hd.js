﻿var pro = pro || {};
(function () {
    pro.Contract = pro.Contract || {};
    pro.Contract.HdPage = pro.Contract.HdPage || {};
    pro.Contract.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Contract.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Contract.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Contract.ListPage.closeTab("");
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
                url: "/PermissionManager/Contract/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Contract.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
               //  $.alertExtend.error();
             }
            );

        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {
          PkId: { required: true  },
          EmployeeCode: { required: true  },
          DepartmentCode: { required: true  },
          BeginDate: { required: true  },
          EndDate: { required: true  },
          Remark: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
          LastModificationTime: { required: true  },
          IsDelete: { required: true  },
          State: { required: true  },
          IsActive: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeCode:  "必填!",
          DepartmentCode:  "必填!",
          BeginDate:  "必填!",
          EndDate:  "必填!",
          Remark:  "必填!",
          CreatorUserCode:  "必填!",
          CreatorUserName:  "必填!",
          CreateTime:  "必填!",
          LastModificationTime:  "必填!",
          IsDelete:  "必填!",
          State:  "1 最初签订 2续订 3 变更 4 终止 必填!",
          IsActive:  "是否有效必填!",
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
    pro.Contract.HdPage.initPage();
});



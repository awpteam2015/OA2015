var pro = pro || {};
(function () {
    pro.EmployeeFile = pro.EmployeeFile || {};
    pro.EmployeeFile.HdPage = pro.EmployeeFile.HdPage || {};
    pro.EmployeeFile.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.EmployeeFile.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.EmployeeFile.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.EmployeeFile.ListPage.closeTab("");
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
                url: "/HRManager/EmployeeFile/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.EmployeeFile.ListPage.closeTab();
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
          EmployeeID: { required: true  },
          FName: { required: true  },
          FileUrl: { required: true  },
          CreatorUserCode: { required: true  },
          CreattorUserName: { required: true  },
          CreationTime: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeID:  "用户ID必填!",
          FName:  "文件名必填!",
          FileUrl:  "文件地址必填!",
          CreatorUserCode:  "创建人必填!",
          CreattorUserName:  "创建人名称必填!",
          CreationTime:  "创建时间必填!",
          LastModificationTime:  "修改时间必填!",
          LastModifierUserCode:  "必填!",
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
    pro.EmployeeFile.HdPage.initPage();
});



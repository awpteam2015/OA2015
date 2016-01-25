var pro = pro || {};
(function () {
    pro.GroupEmployee = pro.GroupEmployee || {};
    pro.GroupEmployee.HdPage = pro.GroupEmployee.HdPage || {};
    pro.GroupEmployee.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.GroupEmployee.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.GroupEmployee.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.GroupEmployee.ListPage.closeTab("");
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
            if (!$("#form1").valid() && !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/GroupEmployee/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.GroupEmployee.ListPage.closeTab();
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
          GroupCode: { required: true  },
          EmployeeCode: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
                    },
                    messages: {
          PkId:  "PkId必填!",
          GroupCode:  "组编号必填!",
          EmployeeCode:  "员工编号必填!",
          CreatorUserCode:  "操作员必填!",
          CreatorUserName:  "操作员必填!",
          CreateTime:  "创建时间必填!",
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
    pro.GroupEmployee.HdPage.initPage();
});



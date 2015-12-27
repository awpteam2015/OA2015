var pro = pro || {};
(function () {
    pro.UserDepartment = pro.UserDepartment || {};
    pro.UserDepartment.HdPage = pro.UserDepartment.HdPage || {};
    pro.UserDepartment.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.UserDepartment.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.UserDepartment.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.UserDepartment.ListPage.closeTab("");
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
                url: "/PermissionManager/UserDepartment/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Function.ListPage.closeTab();
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
          UserCode: { required: true  },
          DepartmentCode: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          UserCode:  "用户ID（工号）必填!",
          DepartmentCode:  "部门代码必填!",
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
    pro.UserDepartment.HdPage.initPage();
});



var pro = pro || {};
(function () {
    pro.Department = pro.Department || {};
    pro.Department.HdPage = pro.Department.HdPage || {};
    pro.Department.HdPage = {
        initPage: function () {

            pro.DepartmentControl.init({ controlId: "ParentDepartmentCode" ,required:true});

            $("#btnAdd").click(function () {
                pro.Department.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Department.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.Department.ListPage.closeTab("");
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                //$('#DepartmentType').combobox('setValue', bindEntity["DepartmentType"]);
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
                url: "/PermissionManager/Department/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Department.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
                 // $.alertExtend.error();
             }
            );
        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {
                        //PkId: { required: true },
                        DepartmentCode: { required: true },
                        DepartmentName: { required: true },
                        ParentDepartmentCode: { required: true }
                        //Remark: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        DepartmentCode: "部门编码必填!",
                        DepartmentName: "部门名称必填!",
                        ParentDepartmentCode: "公司代码必填!",
                        Remark: "备注必填!"
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
    pro.Department.HdPage.initPage();
});



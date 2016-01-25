var pro = pro || {};
(function () {
    pro.EmployeeYearDetail = pro.EmployeeYearDetail || {};
    pro.EmployeeYearDetail.HdPage = pro.EmployeeYearDetail.HdPage || {};
    pro.EmployeeYearDetail.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.EmployeeYearDetail.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.EmployeeYearDetail.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.EmployeeYearDetail.ListPage.closeTab("");
            });
            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            }).combotree({
                onSelect: function (node) {
                    $('#EmployeeCode').combobox({
                        required: true,
                        editable: false,
                        valueField: 'EmployeeCode',
                        textField: 'EmployeeName',
                        url: '/HRManager/EmployeeInfo/GetAllList?DepartmentCode=' + node.DepartmentCode
                    });
                }
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
                url: "/HRManager/EmployeeYearDetail/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.EmployeeYearDetail.ListPage.closeTab();
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
                        PkId: { required: true },
                        DepartmentCode: { required: true },
                        EmployeeCode: { required: true },
                        BeginDate: { required: true },
                        EndDate: { required: true },
                        UseCount: { required: true },
                        BeforeUseCount: { required: true },
                        LeftCount: { required: true },
                        Remark: { required: true },
                        CreatorUserCode: { required: true },
                        CreatorUserName: { required: true },
                        CreateTime: { required: true },
                        LastModificationTime: { required: true },
                    },
                    messages: {
                        PkId: "必填!",
                        DepartmentCode: "部门编号必填!",
                        EmployeeCode: "员工编号必填!",
                        BeginDate: "必填!",
                        EndDate: "必填!",
                        UseCount: "_decimal合计天数必填!",
                        BeforeUseCount: "_decimal使用前天数必填!",
                        LeftCount: "_decimal 年休余数必填!",
                        Remark: "必填!",
                        CreatorUserCode: "必填!",
                        CreatorUserName: "必填!",
                        CreateTime: "必填!",
                        LastModificationTime: "必填!",
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
    pro.EmployeeYearDetail.HdPage.initPage();
});



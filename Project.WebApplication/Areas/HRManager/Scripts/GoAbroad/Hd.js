var pro = pro || {};
(function () {
    pro.GoAbroad = pro.GoAbroad || {};
    pro.GoAbroad.HdPage = pro.GoAbroad.HdPage || {};
    pro.GoAbroad.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.GoAbroad.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.GoAbroad.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.GoAbroad.ListPage.closeTab("");
            });

            $("#EndDate").blur(function () { pro.GoAbroad.HdPage.calcDaySum(); });
            $("#BeginDate").blur(function () { pro.GoAbroad.HdPage.calcDaySum(); });

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
        calcDaySum: function () {

            if (!new Date($("#EndDate").val()).getTime() || !new Date($("#BeginDate").val()).getTime()) {
                $("#DaySum").val(0);
                return;
            }

            var difTime = new Date($("#EndDate").val()).getTime() - new Date($("#BeginDate").val()).getTime();
            var days = Math.floor(difTime / (24 * 3600 * 1000))
            if (days <= 0)
                $("#DaySum").val(0);
            else
                $("#DaySum").val(days + 1);
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.DepartmentName = $('#DepartmentCode').combobox('getText');
            postData.RequestEntity.EmployeeName = $('#EmployeeCode').combobox('getText');
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/GoAbroad/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.GoAbroad.ListPage.closeTab();
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
                        EmployeeCode: { required: true },
                        DepartmentCode: { required: true },
                        Country: { required: true },
                        BeginDate: { required: true },
                        EndDate: { required: true },
                        DaySum: { required: true },
                        Reason: { required: true },
                        Remark: { required: true },
                        CreatorUserCode: { required: true },
                        CreatorUserName: { required: true },
                        CreateTime: { required: true },
                        LastModificationTime: { required: true },
                    },
                    messages: {
                        PkId: "必填!",
                        EmployeeCode: "员工编号必填!",
                        DepartmentCode: "员工部门必填!",
                        Country: "出差国家必填!",
                        BeginDate: "出国开始日期必填!",
                        EndDate: "出国结束日期必填!",
                        DaySum: "出国天数必填!",
                        Reason: "事由必填!",
                        Remark: "备注必填!",
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
    pro.GoAbroad.HdPage.initPage();
});



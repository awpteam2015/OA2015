var pro = pro || {};
(function () {
    pro.Sanction = pro.Sanction || {};
    pro.Sanction.HdPage = pro.Sanction.HdPage || {};
    pro.Sanction.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Sanction.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Sanction.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.Sanction.ListPage.closeTab("");
            });
            $('#SanctionObjType').combobox({
                onSelect: function (node) {
                    if (node.value == '1')
                        $("#SanctionObj+.combo").hide();
                    else
                        $("#SanctionObj+.combo").show();
                }
            })
            $('#SanctionObjLevel').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=JCDJ'
            });
            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            }).combotree({
                onSelect: function (node) {
                    $('#SanctionObj').combobox({
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

            postData.RequestEntity.SanctionObjLevelName = $('#SanctionObjLevel').combobox('getText');
            if (postData.RequestEntity.SanctionObjType == '1') {
                postData.RequestEntity.SanctionObj = postData.RequestEntity.DepartmentCode;
                postData.RequestEntity.SanctionObjName = $('#DepartmentCode').combobox('getText');
            }
            else {
                postData.RequestEntity.SanctionObjName = $('#SanctionObj').combobox('getText');
            }

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/Sanction/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Sanction.ListPage.closeTab();
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
                        SanctionType: { required: true },
                        SanctionObjType: { required: true },
                        //SanctionObj: { required: true },
                        SanctionTitle: { required: true },
                        SanctionMoney: { required: true },
                        SanctionDate: { required: true },
                        Remark: { required: true },
                        CreatorUserCode: { required: true },
                        CreatorUserName: { required: true },
                        CreateTime: { required: true },
                        LastModificationTime: { required: true },
                    },
                    messages: {
                        PkId: "必填!",
                        SanctionType: "奖罚类型必填!",
                        SanctionObjType: "奖罚对象类型必填!",
                        //SanctionObj: "奖罚对象必填!",
                        SanctionTitle: "奖罚名目必填!",
                        SanctionMoney: "奖罚金额必填!",
                        SanctionDate: "奖罚日期必填!",
                        Remark: "备注必填!",
                        CreatorUserCode: "操作人必填!",
                        CreatorUserName: "操作人名称必填!",
                        CreateTime: "创建日期必填!",
                        LastModificationTime: "修改时间必填!",
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
    pro.Sanction.HdPage.initPage();
});



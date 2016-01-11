var pro = pro || {};
(function () {
    pro.EmployeeInfo = pro.EmployeeInfo || {};
    pro.EmployeeInfo.HdPage = pro.EmployeeInfo.HdPage || {};
    pro.EmployeeInfo.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.EmployeeInfo.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.EmployeeInfo.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.EmployeeInfo.ListPage.closeTab("");
            });

            $('#DepartmentCode').combotree({
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            });

            $('#TechnicalTitle').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=JSZC'
            });
            $('#Duties').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=DWZW'
            });
            $('#WorkState').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZZZT'
            });

            $('#EmployeeType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=YGLY'
            });
            $('#State').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZT'
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
            postData.RequestEntity.TechnicalTitleName = $('#TechnicalTitle').combobox('getText');
            postData.RequestEntity.DutiesName = $('#Duties').combobox('getText');
            postData.RequestEntity.WorkStateName = $('#WorkState').combobox('getText');
            postData.RequestEntity.EmployeeTypeName = $('#EmployeeType').combobox('getText');
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/EmployeeInfo/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.EmployeeInfo.ListPage.closeTab();
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

                        EmployeeCode: { required: true },
                        EmployeeName: { required: true },
                        //DepartmentCode: { required: true  },
                        //JobName: { required: true  },
                        //PayCode: { required: true  },
                        //Sex: { required: true  },
                        CertNo: {  isIdCardNo: '输入正确身份证号' },
                       
                        //Birthday: { required: true  },
                        //TechnicalTitle: { required: true  },
                        //Duties: { required: true  },
                        //WorkState: { required: true  },
                        //EmployeeType: { required: true  },
                        //HomeAddress: { required: true  },
                        //MobileNO: {  },
                        //ImageUrl: { required: true  },
                        //Sort: { required: true  },
                        //State: { required: true  },
                        //Remark: { required: true  },
                        //CreatorUserCode: { required: true  },
                        //CreatorUserName: { required: true  },
                        //CreateTime: { required: true  },
                        //LastModificationTime: { required: true  },
                    },
                    messages: {
                        PkId: "必填!",
                        EmployeeCode: "员工编号必填!",
                        EmployeeName: "员工名称必填!",
                        DepartmentCode: "所属部门必填!",
                        JobName: "工号必填!",
                        PayCode: "中文简拼必填!",
                        Sex: "姓别必填!",
                        CertNo: "身份证必填!",
                        Birthday: "生日必填!",
                        TechnicalTitle: "技术职称必填!",
                        Duties: "单位职务必填!",
                        WorkState: "在职状态必填!",
                        EmployeeType: "员工类型必填!",
                        HomeAddress: "家庭地址必填!",
                        MobileNO: "手机号必填!",
                        ImageUrl: "图片地址必填!",
                        Sort: "排序必填!",
                        State: "状态必填!",
                        Remark: "备注必填!",
                        CreatorUserCode: "操作员必填!",
                        CreatorUserName: "操作员名称必填!",
                        CreateTime: "创建时间必填!",
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
    pro.EmployeeInfo.HdPage.initPage();
});



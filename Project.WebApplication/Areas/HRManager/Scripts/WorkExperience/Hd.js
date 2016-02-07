var pro = pro || {};
(function () {
    pro.WorkExperience = pro.WorkExperience || {};
    pro.WorkExperience.HdPage = pro.WorkExperience.HdPage || {};
    pro.WorkExperience.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.WorkExperience.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.WorkExperience.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.WorkExperience.ListPage.closeTab("");
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
            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/WorkExperience/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.WorkExperience.ListPage.closeTab();
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
          WorkCompany: { required: true  },
          Duties: { required: true  },
          BeginDate: { required: true  },
          EndDate: { required: true  },
          WorkContent: { required: true  },
          LeaveReason: { required: true  },
          Remark: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
          LastModificationTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeCode:  "员工编号必填!",
          DepartmentCode:  "所属部门必填!",
          WorkCompany:  "工作单位必填!",
          Duties:  "职务必填!",
          BeginDate:  "开始日期必填!",
          EndDate:  "结束日期必填!",
          WorkContent:  "工作内容必填!",
          LeaveReason:  "离职原因必填!",
          Remark:  "备注必填!",
          CreatorUserCode:  "操作人必填!",
          CreatorUserName:  "操作人名称必填!",
          CreateTime:  "创建时间必填!",
          LastModificationTime:  "修改时间必填!",
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
    pro.WorkExperience.HdPage.initPage();
});



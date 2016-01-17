var pro = pro || {};
(function () {
    pro.LearningExperiences = pro.LearningExperiences || {};
    pro.LearningExperiences.HdPage = pro.LearningExperiences.HdPage || {};
    pro.LearningExperiences.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.LearningExperiences.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.LearningExperiences.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.LearningExperiences.ListPage.closeTab("");
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
                url: "/HRManager/LearningExperiences/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.LearningExperiences.ListPage.closeTab();
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
          ProfessionCode: { required: true  },
          School: { required: true  },
          Degree: { required: true  },
          Education: { required: true  },
          VerifyPersone: { required: true  },
          Reward: { required: true  },
          Certificate: { required: true  },
          Remark: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
          LastModificationTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeCode:  "员工编号必填!",
          DepartmentCode:  "部门编号必填!",
          ProfessionCode:  "专业必填!",
          School:  "学校必填!",
          Degree:  "获取学位必填!",
          Education:  "获取学历必填!",
          VerifyPersone:  "证明人必填!",
          Reward:  "获奖说明必填!",
          Certificate:  "获奖证书必填!",
          Remark:  "备注必填!",
          CreatorUserCode:  "操作人员必填!",
          CreatorUserName:  "操作人员名称必填!",
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
    pro.LearningExperiences.HdPage.initPage();
});



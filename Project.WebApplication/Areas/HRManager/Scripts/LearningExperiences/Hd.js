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
          BeginDate: { required: true  },
          EndDate: { required: true  },
          Remark: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
          LastModificationTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeCode:  "必填!",
          DepartmentCode:  "必填!",
          ProfessionCode:  "专业必填!",
          School:  "学校必填!",
          Degree:  "学位必填!",
          Education:  "学历必填!",
          BeginDate:  "开始时间必填!",
          EndDate:  "结束时间必填!",
          Remark:  "备注必填!",
          CreatorUserCode:  "操作人员必填!",
          CreatorUserName:  "必填!",
          CreateTime:  "必填!",
          LastModificationTime:  "必填!",
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



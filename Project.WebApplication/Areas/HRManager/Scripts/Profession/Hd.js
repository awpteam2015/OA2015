var pro = pro || {};
(function () {
    pro.Profession = pro.Profession || {};
    pro.Profession.HdPage = pro.Profession.HdPage || {};
    pro.Profession.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Profession.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Profession.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Profession.ListPage.closeTab("");
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
                url: "/HRManager/Profession/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Profession.ListPage.closeTab();
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
          EmployeeID: { required: true  },
          EmployeeCode: { required: true  },
          DepartmentCode: { required: true  },
          Title: { required: true  },
          TypeName: { required: true  },
          RangeName: { required: true  },
          GetDate: { required: true  },
          CerNo: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreationTime: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeID:  "必填!",
          EmployeeCode:  "员工编号必填!",
          DepartmentCode:  "部门编号必填!",
          Title:  "名称必填!",
          TypeName:  "类别必填!",
          RangeName:  "范围必填!",
          GetDate:  "取得时间必填!",
          CerNo:  "职称证书编号必填!",
          CreatorUserCode:  "操作人员必填!",
          CreatorUserName:  "操作人员名称必填!",
          CreationTime:  "创建时间必填!",
          LastModificationTime:  "修改时间必填!",
          LastModifierUserCode:  "修改人必填!",
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
    pro.Profession.HdPage.initPage();
});



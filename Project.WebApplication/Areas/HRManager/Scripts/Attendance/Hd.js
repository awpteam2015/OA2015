var pro = pro || {};
(function () {
    pro.Attendance = pro.Attendance || {};
    pro.Attendance.HdPage = pro.Attendance.HdPage || {};
    pro.Attendance.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Attendance.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Attendance.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Attendance.ListPage.closeTab("");
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
                url: "/PermissionManager/Attendance/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Attendance.ListPage.closeTab();
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
          AttendanceUploadRecordId: { required: true  },
          EmployeeCode: { required: true  },
          DepartmentCode: { required: true  },
          DepartmentName: { required: true  },
          State: { required: true  },
          Date: { required: true  },
          Remark: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
          IsDelete: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          AttendanceUploadRecordId:  "通过那次上传 0代表自己补的必填!",
          EmployeeCode:  "必填!",
          DepartmentCode:  "必填!",
          DepartmentName:  "必填!",
          State:  "-1代表缺勤 1代表正常必填!",
          Date:  "考勤日期必填!",
          Remark:  "必填!",
          CreatorUserCode:  "必填!",
          CreatorUserName:  "必填!",
          CreateTime:  "必填!",
          IsDelete:  "必填!",
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
    pro.Attendance.HdPage.initPage();
});



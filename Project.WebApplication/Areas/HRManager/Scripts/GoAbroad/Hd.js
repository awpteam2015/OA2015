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
          PkId: { required: true  },
          EmployeeCode: { required: true  },
          DepartmentCode: { required: true  },
          Country: { required: true  },
          BeginDate: { required: true  },
          EndDate: { required: true  },
          DaySum: { required: true  },
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
          Country:  "必填!",
          BeginDate:  "必填!",
          EndDate:  "必填!",
          DaySum:  "必填!",
          Remark:  "必填!",
          CreatorUserCode:  "必填!",
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
    pro.GoAbroad.HdPage.initPage();
});



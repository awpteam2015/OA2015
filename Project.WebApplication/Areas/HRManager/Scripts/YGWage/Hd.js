var pro = pro || {};
(function () {
    pro.YGWage = pro.YGWage || {};
    pro.YGWage.HdPage = pro.YGWage.HdPage || {};
    pro.YGWage.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.YGWage.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.YGWage.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.YGWage.ListPage.closeTab("");
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
                url: "/HRManager/YGWage/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.YGWage.ListPage.closeTab();
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
          GWGZ: { required: true  },
          XZGZ: { required: true  },
          CreatorUserCode: { required: true  },
          CreattorUserName: { required: true  },
          CreationTime: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          EmployeeID:  "必填!",
          GWGZ:  "必填!",
          XZGZ:  "必填!",
          CreatorUserCode:  "必填!",
          CreattorUserName:  "必填!",
          CreationTime:  "必填!",
          LastModificationTime:  "必填!",
          LastModifierUserCode:  "必填!",
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
    pro.YGWage.HdPage.initPage();
});



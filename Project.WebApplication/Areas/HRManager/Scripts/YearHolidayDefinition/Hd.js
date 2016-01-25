var pro = pro || {};
(function () {
    pro.YearHholidayDefinition = pro.YearHholidayDefinition || {};
    pro.YearHholidayDefinition.HdPage = pro.YearHholidayDefinition.HdPage || {};
    pro.YearHholidayDefinition.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.YearHholidayDefinition.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.YearHholidayDefinition.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.YearHholidayDefinition.ListPage.closeTab("");
            });

            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                $('#BeginMonth').numberbox('setValue', bindEntity['BeginMonth']);
                $('#EndMonth').numberbox('setValue', bindEntity['EndMonth']);
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
            if (!$("#form1").valid() && !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/YearHolidayDefinition/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.YearHholidayDefinition.ListPage.closeTab();
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
          YearsNum: { required: true  },
          BeginMonth: { required: true  },
          EndMonth: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
          LastModificationTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          YearsNum:  "年份必填!",
          BeginMonth:  "开始月必填!",
          EndMonth:  "结束月必填!",
          CreatorUserCode:  "操作人必填!",
          CreatorUserName:  "操作人姓名必填!",
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
    pro.YearHholidayDefinition.HdPage.initPage();
});



var pro = pro || {};
(function () {
    pro.HolidayDetail = pro.HolidayDetail || {};
    pro.HolidayDetail.HdPage = pro.HolidayDetail.HdPage || {};
    pro.HolidayDetail.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.HolidayDetail.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.HolidayDetail.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.HolidayDetail.ListPage.closeTab("");
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
                url: "/SystemSetManager/HolidayDetail/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.HolidayDetail.ListPage.closeTab();
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
          HolidayName: { required: true  },
          HolidayDate: { required: true  },
          //Remark: { required: true  },
          //CreatorUserCode: { required: true  },
          //CreatorUserName: { required: true  },
          //CreateTime: { required: true  },
          //LastModificationTime: { required: true  },
                    },
                    messages: {
          PkId:  "PkId必填!",
          HolidayName:  "必填!",
          HolidayDate:  "日期必填!",
          Remark:  "备注必填!",
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
    pro.HolidayDetail.HdPage.initPage();
});



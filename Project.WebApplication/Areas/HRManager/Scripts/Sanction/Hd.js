var pro = pro || {};
(function () {
    pro.Sanction = pro.Sanction || {};
    pro.Sanction.HdPage = pro.Sanction.HdPage || {};
    pro.Sanction.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.Sanction.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Sanction.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.Sanction.ListPage.closeTab("");
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
                url: "/HRManager/Sanction/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Sanction.ListPage.closeTab();
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
          SanctionType: { required: true  },
          SanctionObj: { required: true  },
          SanctionTitle: { required: true  },
          SanctionMoney: { required: true  },
          SanctionDate: { required: true  },
          Remark: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreateTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          SanctionType:  "0:罚 1:奖必填!",
          SanctionObj:  "0:个人 1:部门（科室）必填!",
          SanctionTitle:  "必填!",
          SanctionMoney:  "必填!",
          SanctionDate:  "必填!",
          Remark:  "必填!",
          CreatorUserCode:  "必填!",
          CreatorUserName:  "必填!",
          CreateTime:  "必填!",
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
    pro.Sanction.HdPage.initPage();
});



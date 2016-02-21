var pro = pro || {};
(function () {
    pro.MessageInfo = pro.MessageInfo || {};
    pro.MessageInfo.HdPage = pro.MessageInfo.HdPage || {};
    pro.MessageInfo.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.MessageInfo.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.MessageInfo.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.MessageInfo.ListPage.closeTab("");
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
                url: "/HRManager/MessageInfo/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.MessageInfo.ListPage.closeTab();
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
          MesTitle: { required: true  },
          MesContent: { required: true  },
          ReceiveUserCode: { required: true  },
          IsAll: { required: true  },
          CreatorUserCode: { required: true  },
          CreatorUserName: { required: true  },
          CreationTime: { required: true  },
          LastModificationTime: { required: true  },
          LastModifierUserCode: { required: true  },
          DeleterUserCode: { required: true  },
          DeletionTime: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          MesTitle:  "标题必填!",
          MesContent:  "内容必填!",
          ReceiveUserCode:  "接收人必填!",
          IsAll:  "是否所有人必填!",
          CreatorUserCode:  "发送人必填!",
          CreatorUserName:  "发送姓名必填!",
          CreationTime:  "必填!",
          LastModificationTime:  "必填!",
          LastModifierUserCode:  "必填!",
          DeleterUserCode:  "必填!",
          DeletionTime:  "必填!",
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
    pro.MessageInfo.HdPage.initPage();
});



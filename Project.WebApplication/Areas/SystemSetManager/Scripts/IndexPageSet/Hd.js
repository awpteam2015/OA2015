var pro = pro || {};
(function () {
    pro.IndexPageSet = pro.IndexPageSet || {};
    pro.IndexPageSet.HdPage = pro.IndexPageSet.HdPage || {};
    pro.IndexPageSet.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.IndexPageSet.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.IndexPageSet.HdPage.submit("Edit");
            });
            
             $("#btnClose").click(function () {
                parent.pro.IndexPageSet.ListPage.closeTab("");
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
            postData.RequestEntity.Des = Base64.encode($("#Des").val());
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/SystemSetManager/IndexPageSet/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                   //function afterSuccess() {
                   //     parent.$("#btnSearch").trigger("click");
                   //     parent.pro.IndexPageSet.ListPage.closeTab();
                   // }
                   // parent.$.alertExtend.info("", afterSuccess());
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
          Des: { required: true  },
                    },
                    messages: {
          PkId:  "必填!",
          Des:  "必填!",
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
    pro.IndexPageSet.HdPage.initPage();
});



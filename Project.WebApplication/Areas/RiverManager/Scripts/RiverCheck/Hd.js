var pro = pro || {};
(function () {
    pro.RiverCheck = pro.RiverCheck || {};
    pro.RiverCheck.HdPage = pro.RiverCheck.HdPage || {};
    pro.RiverCheck.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RiverCheck.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RiverCheck.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.RiverCheck.ListPage.closeTab("");
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



            $('#RiverId').combobox({
                required: true,
                editable: false,
                valueField: 'PkId',
                textField: 'RiverName',
                url: '/RiverManager/River/GetListNoPage',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#RiverId").combobox('setValue', bindEntity['RiverId']);
                    }
                }
            });


            $('#UserCode').combobox({
                required: true,
                editable: false,
                valueField: 'UserCode',
                textField: 'UserName',
                url: '/PermissionManager/UserInfo/GetListNoPage',
                onLoadSuccess: function () {
                    if (pro.commonKit.getUrlParam("PkId") > 0) {
                        $("#UserCode").combobox('setValue', bindEntity['UserCode']);
                    }
                }
            });

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
                url: "/RiverManager/RiverCheck/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RiverCheck.ListPage.closeTab();
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
                        RiverId: { required: true },
                        UserCode: { required: true },
                        Coords: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        RiverId: "必填!",
                        RiverName: "必填!",
                        UserName: "必填!",
                        UserCode: "必填!",
                        Coords: "控制点必填!",
                        IsActive: "是否有效必填!",
                        CreatorUserCode: "创建人必填!",
                        CreationTime: "创建时间必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "修改时间必填!",
                        Remark: "备注必填!",
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
    pro.RiverCheck.HdPage.initPage();
});



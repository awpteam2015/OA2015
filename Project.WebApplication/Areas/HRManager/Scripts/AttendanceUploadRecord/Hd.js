var pro = pro || {};
(function () {
    pro.AttendanceUploadRecord = pro.AttendanceUploadRecord || {};
    pro.AttendanceUploadRecord.HdPage = pro.AttendanceUploadRecord.HdPage || {};
    pro.AttendanceUploadRecord.HdPage = {
        initPage: function () {

            pro.DepartmentControl.init();

            $("#btnAdd").click(function () {
                pro.AttendanceUploadRecord.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.AttendanceUploadRecord.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.AttendanceUploadRecord.ListPage.closeTab("");
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
            alert( $("#DepartmentCode").combotree("getText"));
            postData.RequestEntity.DepartmentName = $("#DepartmentCode").combotree("getText");

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/AttendanceUploadRecord/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.AttendanceUploadRecord.ListPage.closeTab();
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
                        DepartmentCode: { required: true },
                        Date: { required: true },
                        Remark: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        DepartmentCode: "必填!",
                        Date: "必填!",
                        Remark: "必填!",
                        CreatorUserCode: "必填!",
                        CreatorUserName: "必填!",
                        CreateTime: "必填!",
                        FileUrl: "必填!",
                        IsDelete: "必填!"
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
    pro.AttendanceUploadRecord.HdPage.initPage();
});



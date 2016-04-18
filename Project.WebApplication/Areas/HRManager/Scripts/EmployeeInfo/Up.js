var pro = pro || {};
(function () {
    pro.EmployeeInfo = pro.EmployeeInfo || {};
    pro.EmployeeInfo.UpPage = pro.EmployeeInfo.UpPage || {};
    pro.EmployeeInfo.UpPage = {
        initPage: function () {

            pro.DepartmentControl.init();

            $("#btnAdd").click(function () {
                pro.EmployeeInfo.UpPage.submit("Upload");
            });

            $("#btnClose").click(function () {
                parent.pro.EmployeeInfo.ListPage.closeTab("");
            });

        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            postData.RequestEntity.DepartmentName = $("#DepartmentCode").combotree("getText");

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();

            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/EmployeeInfo/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.EmployeeInfo.ListPage.closeTab();
                    }

                  
                    parent.$.alertExtend.info(data.error.message, afterSuccess());
                    //parent.$.alertExtend.info("", afterSuccess());
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
                        DepartmentCode: { required: true }
                        //,Date: { required: true }
                        // ,Remark: { required: true }
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
                if ($("#FileUrl").val() == "") {
                    alert("请选择上传文件！");
                    return false;
                }
                return true;
            }
        },

        addTab: function (subtitle, url) {

        }

    };
})();



$(function () {
    pro.EmployeeInfo.UpPage.initPage();
});



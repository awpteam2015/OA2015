var pro = pro || {};
(function () {
    pro.Contract = pro.Contract || {};
    pro.Contract.UploadPage = pro.Contract.UploadPage || {};
    pro.Contract.UploadPage = {
        initPage: function () {

        

            $("#btnUpload").click(function () {
                pro.Contract.UploadPage.submit("Upload");
            });


            $("#btnClose").click(function () {
                parent.pro.Contract.ListPage.closeTab("");
            });

           
        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();

            this.submitExtend.addRule();

            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/Contract/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.Contract.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
                 //alert();
                 //$.alertExtend.error(errormessage);
             }
            );

        },
        submitExtend: {
            addRule: function () {
               
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
    pro.Contract.UploadPage.initPage();
});



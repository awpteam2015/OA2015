var pro = pro || {};
(function () {
    pro.EmployeeInfoDutiesTransfer = pro.EmployeeInfoDutiesTransfer || {};
    pro.EmployeeInfoDutiesTransfer.HdPage = pro.EmployeeInfoDutiesTransfer.HdPage || {};
    pro.EmployeeInfoDutiesTransfer.HdPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase()
            };
        },
        initPage: function () {
            var initObj = this.init();

            $("#btnClose").click(function () {
                parent.pro.EmployeeInfoDutiesTransfer.ListPage.closeTab("");
            });

            $("#btnEdit").click(function () {
                pro.EmployeeInfoDutiesTransfer.HdPage.submit("Edit");
            });

            //职务初始化
            pro.DutiesControl.init();


            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();

                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }
                if (bindEntity["DutiesName"] != undefined && bindEntity["DutiesName"] != "") {

                    $("#OldDuties").val(bindEntity["DutiesName"]);
                }

                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }
        },
        submit: function (command) {
            var postData = {};

            postData.RequestEntity = pro.submitKit.getHeadJson();
            //postData.RequestEntity.TechnicalTitleName = $('#TechnicalTitle').combobox('getText');

            postData.RequestEntity.DutiesName = $('#Duties').combotree('getText');
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();

            if (!$("#form1").valid() || !this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/HRManager/EmployeeInfoDutiesTransfer/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.EmployeeInfoDutiesTransfer.ListPage.closeTab();
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

                        EmployeeCode: { required: true },
                        EmployeeName: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        EmployeeCode: "员工编号必填!",
                        EmployeeName: "员工名称必填!"
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
    pro.EmployeeInfoDutiesTransfer.HdPage.initPage();
});



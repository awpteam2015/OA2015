var pro = pro || {};
(function () {
    pro.RiverProblemApply = pro.RiverProblemApply || {};
    pro.RiverProblemApply.HdPage = pro.RiverProblemApply.HdPage || {};
    pro.RiverProblemApply.HdPage = {
        initPage: function () {
            $("#btnAdd").click(function () {
                pro.RiverProblemApply.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.RiverProblemApply.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.RiverProblemApply.ListPage.closeTab("");
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
            postData.RequestEntity.DepartmentName = $('#DepartmentCode').text();
            postData.RequestEntity.UserName = $('#UserCode').text();
            postData.RequestEntity.RiverName = $('#RiverId').combobox('getText');
            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/RiverManager/RiverProblemApply/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                        parent.pro.RiverProblemApply.ListPage.closeTab();
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
                        PkId: { required: true },
                        Title: { required: true },
                        Des: { required: true },
                        ProblemType: { required: true },
                        PicUrl: { required: true },
                        DepartmentCode: { required: true },
                        RiverId: { required: true },
                        RiverName: { required: true },
                        UserCode: { required: true },
                        UserName: { required: true },
                        Coords: { required: true },
                        State: { required: true },
                        DepartmentRemark: { required: true },
                        DepartmentOpTime: { required: true },
                        TopDepartmentRemark: { required: true },
                        TopDepartmentOpTime: { required: true },
                        FinishOpTime: { required: true },
                        FinishRemark: { required: true },
                        ReturnRemark: { required: true },
                        ReturnOpTime: { required: true },
                        IsExposure: { required: true },
                        ExposureLever: { required: true },
                        IsSendMessage: { required: true },
                        IsActive: { required: true },
                        CreatorUserName: { required: true },
                        CreatorUserCode: { required: true },
                        CreationTime: { required: true },
                        LastModifierUserName: { required: true },
                        LastModifierUserCode: { required: true },
                        LastModificationTime: { required: true },
                        Remark: { required: true },
                        DeleteRemark: { required: true },
                        IsDeleted: { required: true },
                        DeleteUserName: { required: true },
                        DeleteUserCode: { required: true },
                        DeleteTime: { required: true },
                    },
                    messages: {
                        PkId: "必填!",
                        Title: "必填!",
                        Des: "问题描述必填!",
                        ProblemType: "问题类型 1日常巡河 2问题上报必填!",
                        PicUrl: "图片地址 多个必填!",
                        DepartmentCode: "所属部门必填!",
                        RiverId: "必填!",
                        RiverName: "必填!",
                        UserCode: "必填!",
                        UserName: "必填!",
                        Coords: "坐标必填!",
                        State: "问题状态 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 必填!",
                        DepartmentRemark: "部门转发备注必填!",
                        DepartmentOpTime: "部门操作时间必填!",
                        TopDepartmentRemark: "顶级部门批注必填!",
                        TopDepartmentOpTime: "顶级部门批注时间必填!",
                        FinishOpTime: "河长结束问题时间必填!",
                        FinishRemark: "河长结束问题备注必填!",
                        ReturnRemark: "河长回退问题备注必填!",
                        ReturnOpTime: "河长回退问题时间必填!",
                        IsExposure: "是否曝光必填!",
                        ExposureLever: "曝光等级必填!",
                        IsSendMessage: "是否已发送短信必填!",
                        IsActive: "是否有效必填!",
                        CreatorUserName: "必填!",
                        CreatorUserCode: "创建人必填!",
                        CreationTime: "创建时间必填!",
                        LastModifierUserName: "必填!",
                        LastModifierUserCode: "修改人必填!",
                        LastModificationTime: "修改时间必填!",
                        Remark: "备注必填!",
                        DeleteRemark: "删除原因必填!",
                        IsDeleted: "是否删除必填!",
                        DeleteUserName: "删除人必填!",
                        DeleteUserCode: "删除人编码必填!",
                        DeleteTime: "删除时间必填!",
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
    pro.RiverProblemApply.HdPage.initPage();
});



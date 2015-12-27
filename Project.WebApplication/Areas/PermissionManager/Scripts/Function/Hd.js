var pro = pro || {};
(function () {
    pro.Function = pro.Function || {};
    pro.Function.HdPage = pro.Function.HdPage || {};
    pro.Function.HdPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false)
            };
        },
        initPage: function () {
            var initObj = this.init();
            var tabObj = initObj.tabObj;
            var gridObj = initObj.gridObj;
            gridObj.grid({
                url: '/PermissionManager/Function/GetFunctionDetailList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
                    { field: 'PkId', title: '', width: 100, formatter: function (value, row, index) {
                        return pro.controlKit.getInputHtml("PkId", value);
                    }
         },
                    {
                        field: 'FunctionDetailName', title: '功能名称', width: 100, formatter: function (value, row, index) {
                            return pro.controlKit.getInputHtml("FunctionDetailName_" + row.PkId, value);
                        }
                    },
                    {
                        field: 'FunctionDetailCode', title: '按钮Id', width: 100, formatter: function (value, row, index) {
                            return pro.controlKit.getInputHtml("FunctionDetailCode_" + row.PkId, value);
                        }
                    },
                    {
                        field: 'Area', title: 'Area', width: 100, formatter: function (value, row, index) {
                            return pro.controlKit.getInputHtml("Area_" + row.PkId, value);
                        }
                    },
                    {
                        field: 'Controller', title: 'Controller', width: 100, formatter: function (value, row, index) {
                            return pro.controlKit.getInputHtml("Controller_" + row.PkId, value);
                        }
                    },
                    {
                        field: 'Action', title: 'Action', width: 100, formatter: function (value, row, index) {
                            return pro.controlKit.getInputHtml("Action_" + row.PkId, value);
                        }
                    }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $("#btnAdd_ToolBar").click(function () {
                gridObj.insertRow({
                    PkId: gridObj.PkId,
                    FunctionDetailCode: ""
                });
            });


            $("#btnDel_ToolBar").click(function () {
                gridObj.delRow();
            });


            $("#btnAdd").click(function () {
                pro.Function.HdPage.submit("Add");
            });

            $("#btnEdit").click(function () {
                pro.Function.HdPage.submit("Edit");
            });

            $("#btnClose").click(function () {
                parent.pro.Function.ListPage.closeTab("");
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
            pro.submitKit.config.columnPkidName = "PkId";
            pro.submitKit.config.columns = ["FunctionDetailName", "FunctionDetailCode", "Area", "Controller", "Action"];
            postData.RequestEntity.FunctionDetailList = pro.submitKit.getRowJson();

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && pro.submitExtend.logicValidate()) {
                $.alertExtend.error();
                return false;
            }

            abp.ajax({
                url: "/PermissionManager/Function/" + command,
                data: JSON.stringify(postData)
            }).done(
                function (dataresult, data) {
                    function afterSuccess() {
                        parent.$("#btnSearch").trigger("click");
                       // parent.pro.Function.ListPage.closeTab();
                    }
                    parent.$.alertExtend.info("", afterSuccess());
                }
            ).fail(
             function (errordetails, errormessage) {
                 $.alertExtend.error();
             }
            );

        },
        submitExtend: {
            addRule: function () {
                $("#form1").validate({
                    rules: {
                        PkId: { required: true },
                        FunctionnName: { required: true },
                        ModuleId: { required: true },
                        FunctionUrl: { required: true },
                        Area: { required: true },
                        Controller: { required: true },
                        Action: { required: true },
                        IsDisplayOnMenu: { required: true }
                    },
                    messages: {
                        PkId: "必填!",
                        FunctionnName: "模块名称必填!",
                        ModuleId: "模块ID必填!",
                        FunctionUrl: "模块路径必填!",
                        Area: "必填!",
                        Controller: "必填!",
                        Action: "必填!",
                        IsDisplayOnMenu: "是否在菜单上显示1是 0不是必填!",
                        RankId: "顺序必填!",
                        Remark: "备注必填!"
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
    pro.Function.HdPage.initPage();
});



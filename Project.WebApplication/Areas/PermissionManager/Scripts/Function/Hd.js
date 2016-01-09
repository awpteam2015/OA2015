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
                url: '/PermissionManager/Function/GetFunctionDetailList?FunctionId=' + pro.commonKit.getUrlParam("PkId"),
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                idField: "PkId",
                columns: [
                    [
                        {
                            field: 'PkId', title: '', hidden: true, width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("PkId", value);
                            }
                        },
                        {
                            field: 'FunctionDetailName',
                            title: '功能名称',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("FunctionDetailName_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'FunctionDetailCode',
                            title: '按钮Id',
                            width: 100,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("FunctionDetailCode_" + row.PkId, value);
                            }
                        },
                        {
                            field: 'Area',
                            title: 'Area',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("Area_" + row.PkId, value, 200);
                            }
                        },
                        {
                            field: 'Controller',
                            title: 'Controller',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("Controller_" + row.PkId, value, 200);
                            }
                        },
                        {
                            field: 'Action',
                            title: 'Action',
                            width: 200,
                            formatter: function (value, row, index) {
                                return pro.controlKit.getInputHtml("Action_" + row.PkId, value, 200);
                            }
                        }
                    ]
                ],
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
            );

            $("#btnAdd_ToolBar").click(function () {
                gridObj.insertRow({
                    PkId: gridObj.PkId,
                    FunctionDetailCode: ""
                });

                //console.log(JSON.stringify($("#datagrid").datagrid('getRows')));
                //console.log(gridObj.PkId + 1);

                $("#datagrid").datagrid('selectRecord', gridObj.PkId + 1);
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

            $('#ModuleId').combobox({
                required: true,
                editable: false,
                valueField: 'PkId',
                textField: 'ModuleName',
                url: '/PermissionManager/Module/GetListAll_ForCombobox'
            });


            if ($("#BindEntity").val()) {
                var bindField = pro.bindKit.getHeadJson();
                var bindEntity = JSON.parse($("#BindEntity").val());
                for (var filedname in bindField) {
                    $("[name=" + filedname + "]").val(bindEntity[filedname]);
                }

                $('#ModuleId').combobox('setValue', bindEntity.ModuleId);

                //行项目信息用json绑定控件
                //alert(JSON.stringify(BindEntity.List));
            }

            var moduleId = pro.commonKit.getUrlParam("ModuleId");
            if (moduleId > 0) {
                $('#ModuleId').combobox('setValue', moduleId);
            }

        },
        submit: function (command) {
            var postData = {};
            postData.RequestEntity = pro.submitKit.getHeadJson();
            // postData.RequestEntity.ModuleId = $('#ModuleId').combobox('getValue');

            pro.submitKit.config.columnPkidName = "PkId";
            pro.submitKit.config.columns = ["FunctionDetailName", "FunctionDetailCode", "Area", "Controller", "Action"];
            postData.RequestEntity.FunctionDetailList = pro.submitKit.getRowJson();

            if (pro.commonKit.getUrlParam("PkId") != "") {
                postData.RequestEntity.PkId = pro.commonKit.getUrlParam("PkId");
            }

            this.submitExtend.addRule();
            if (!$("#form1").valid() && this.submitExtend.logicValidate()) {
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
                        parent.pro.Function.ListPage.closeTab();
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
                        //Area: { required: true },
                        //Controller: { required: true },
                        //Action: { required: true },
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

                $("input[name=PkId]").each(function () {
                    var i = $(this).val();
                    $("input[name=FunctionDetailName_" + i + "]").rules("add", { required: true, messages: { required: "必填!" } });
                    $("input[name=FunctionDetailCode_" + i + "]").rules("add", { required: true, messages: { required: "必填!" } });
                    $("input[name=Area_" + i + "]").rules("add", { required: true, messages: { required: "必填!" } });
                    $("input[name=Controller_" + i + "]").rules("add", { required: true, messages: { required: "必填!" } });
                    $("input[name=Action_" + i + "]").rules("add", { required: true, messages: { required: "必填!" } });
                }

                );

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



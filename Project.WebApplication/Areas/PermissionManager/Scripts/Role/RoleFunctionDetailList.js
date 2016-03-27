
var pro = pro || {};
(function () {
    pro.Role = pro.Role || {};
    pro.Role.RoleFunctionDetailListPage = pro.Role.RoleFunctionDetailListPage || {};
    pro.Role.RoleFunctionDetailListPage = {
        init: function () {
            return {
                tabObj: new pro.TabBase(),
                gridObj: new pro.GridBase("#datagrid", false),
                gridObj2: new pro.GridBase("#datagrid2", false),
                //gridObj3: new pro.GridBase("#datagrid3", true)
            };
        },
        initPage: function () {
            var initObj = this.init();

            initObj.gridObj.grid({
                url: '/PermissionManager/Role/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '角色ID', width: 100 },
         { field: 'RoleName', title: '角色名称', width: 100 },
         { field: 'Remark', title: '备注', width: 100 }
                ]],
                onClickRow: function (index, row) {

                    initObj.gridObj2.reload({ RoleId: row.PkId });


                    //initObj.gridObj3.grid({
                    //    url: '/PermissionManager/Department/GetList?RoleId=' + row.PkId,
                    //    idField: "DepartmentCode",
                    //    treeField: "DepartmentCode",
                    //    fitColumns: false,
                    //    nowrap: true,
                    //    rownumbers: false, //行号
                    //    singleSelect: true,
                    //    columns: [[
                    //    {
                    //        field: 'DepartmentCode', title: '部门编码', width: 150, formatter: function (value, row) {
                    //            var checkHtml = row.Attr_IsCheck ? 'checked="checked"' : "";
                    //            return '<input  name="DepartmentCode" type="checkbox" value="' + row.DepartmentCode + '" ' + checkHtml + '/>' + row.DepartmentCode;
                    //        }
                    //    },
                    //    { field: 'DepartmentName', title: '部门名称', width: 100 }
                    //    ]],
                    //    onLoadSuccess: function () {
                    //        $("input[name=DepartmentCode]").click(function () {

                    //            var postData = {};
                    //            postData.RolePkId = RoleId;
                    //            postData.DepartmentCode = $(this).val();
                    //            postData.IsCheck = $(this).is(':checked') ? 1 : 0;

                    //            abp.ajax({
                    //                contentType: abp.ajax.contentTypeForm,
                    //                url: "/PermissionManager/Department/SetRowDepart",
                    //                data: postData
                    //            }).done(
                    //            function (dataresult, data) {
                    //                $("input[name=FunctionDetail_" + postData.FunctionPkId + "]").attr("checked", postData.IsCheck == 1);
                    //            }
                    //            ).fail(
                    //            function (errordetails, errormessage) {
                    //                // $.alertExtend.error(errormessage);
                    //            }
                    //            );
                    //        });

                    //}
                    //}
                    //);

                },
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            initObj.gridObj2.grid({
                view: detailview,
                url: '/PermissionManager/Module/GetListAll',
                fitColumns: false,
                nowrap: true,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
                { field: 'PkId', title: '模块PkId', width: 100 },
                { field: 'ModuleName', title: '模块', width: 600 }
                ]],
                detailFormatter: function (index, row) {
                    return '<div style="padding:2px"><table   id="ddv-' + index + '"></table></div>';
                },
                onExpandRow: function (index, row) {
                    var RoleId = row.Att_RoleId;
                    if (RoleId <= 0) {
                        $.alertExtend.info("请先选择角色！");
                        return false;
                    }

                    $('#ddv-' + index).datagrid({
                        url: '/PermissionManager/Function/GetListAll?RoleId=' + RoleId + '&ModuleId=' + row.PkId,
                        fitColumns: false,
                        singleSelect: true,
                        height: 'auto',
                        width: 600,
                        columns: [[

                        { field: 'PkId', title: '功能页PkId', width: 100 },
                        {
                            field: 'FunctionnName', title: '功能页', width: 100, formatter: function (value, row) {
                                return '<input class="FunctionPkId" name="FunctionPkId" type="checkbox" value="' + row.PkId + '"/>' + row.FunctionnName;

                            }
                        },
                        {
                            field: 'FunctionDetailList', title: '功能详情', width: 300, formatter: function (value, row) {

                                var html = '<div style="width:600;" >';
                                $.each(value, function (index, row) {
                                    var checkHtml = (row.Attr_IsCheck == true ? 'checked="checked"' : "");
                                    html += '<div style="float:left"><input name="FunctionDetail_' + row.FunctionId + '" type="checkbox" value="' + row.PkId + '" ' + checkHtml + '/>' + row.FunctionDetailName + '</div>';

                                });
                                html += '</div>';

                                return html;
                            }
                        }

                        ]],
                        onResize: function () {
                            $('#datagrid2').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {
                            $("input[name=FunctionPkId]").click(function () {

                                var postData = {};
                                postData.RolePkId = RoleId;
                                postData.FunctionPkId = $(this).val();
                                postData.FunctionDetailPkId = 0;
                                postData.IsCheck = $(this).is(':checked') ? 1 : 0;

                                abp.ajax({
                                    contentType: abp.ajax.contentTypeForm,
                                    url: "/PermissionManager/Role/SetRowFunction",
                                    data: postData
                                }).done(
                                function (dataresult, data) {
                                    $("input[name=FunctionDetail_" + postData.FunctionPkId + "]").attr("checked", postData.IsCheck == 1);
                                }
                                ).fail(
                                function (errordetails, errormessage) {
                                    // $.alertExtend.error(errormessage);
                                }
                                );
                                ;

                                //var result = $.ajax({
                                //    url: "/PermissionManager/Role/SetRowFunction",
                                //    type: "POST",
                                //    data: postData,
                                //    async: false,
                                //    cache: false
                                //}).responseText;
                            });

                            $("input[name^=FunctionDetail_]").click(function () {
                                var postData = {};
                                postData.RolePkId = RoleId;
                                postData.FunctionPkId = 0;
                                postData.FunctionDetailPkId = $(this).val();
                                postData.IsCheck = $(this).is(':checked') ? 1 : 0;

                                abp.ajax({
                                    contentType: abp.ajax.contentTypeForm,
                                    url: "/PermissionManager/Role/SetRowFunction",
                                    data: postData
                                });
                            });


                            setTimeout(function () {
                                $('#datagrid2').datagrid('fixDetailRowHeight', index);
                            }, 0);
                        },
                        onBeforeLoad: function () {


                        }
                    });
                },
                pagination: false,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
             );



            $("#btnRefresh").click(function () {
                initObj.gridObj.refresh();
            });
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.Role.RoleFunctionDetailListPage.initPage();



});



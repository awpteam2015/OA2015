﻿
var pro = pro || {};
(function () {
    pro.EmployeeInfo = pro.EmployeeInfo || {};
    pro.EmployeeInfo.ListPage = pro.EmployeeInfo.ListPage || {};
    pro.EmployeeInfo.ListPage = {
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
                url: '/HRManager/EmployeeInfo/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                frozenColumns: [[
                     {
                         field: 'EmployeeCode', title: '员工编号', width: 100, align: 'center',
                         styler: function (value, row, index) {
                             return 'vertical-align:middle;';
                         }
                     },
                     {
                         field: 'EmployeeName', title: '员工名称', width: 100, align: 'center',
                         styler: function (value, row, index) {
                             return 'vertical-align:middle;';
                         }
                     },
                     {
                         field: 'DepartmentName', title: '所属部门', width: 200, align: 'center',
                         styler: function (value, row, index) {
                             return 'vertical-align:middle;';
                         }
                     }
                ]],
                columns: [[
         //{ field: 'PkId', title: '', hidden: true, width: 100 },
         {
             field: 'JobName', title: '工号', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         //{ field: 'PayCode', title: '中文简拼', width: 100 },
         {
             field: 'Sex', title: '姓别', width: 80, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '女'
                         break;
                     case 1:
                         ret = '男'
                         break;
                     default:
                         ret = '未知'
                         break;
                 }
                 return ret;
             }
         },
         {
             field: 'CertNo', title: '身份证', width: 150, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         {
             field: 'Birthday', title: '生日', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         {
             field: 'DutiesName', title: '单位职务', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         {
             field: 'WorkStateName', title: '在职状态', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         {
             field: 'EmployeeTypeName', title: '员工类型', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         {
             field: 'PoliticsName', title: '政治面貌', width: 160, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         
         //{ field: 'HomeAddress', title: '家庭地址', width: 100 },
         //{ field: 'MobileNO', title: '手机号', width: 100 },
         //{ field: 'ImageUrl', title: '图片地址', width: 100 },
         {
             field: 'Sort', title: '排序', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         //{
         //    field: 'State', title: '状态', width: 100, align: 'center',
         //    styler: function (value, row, index) {
         //        return 'vertical-align:middle;';
         //    }, formatter: function (value, row, index) {
         //        var ret = "";
         //        switch (value) {
         //            case 0:
         //                ret = '停用'
         //                break;
         //            case 1:
         //                ret = '启用'
         //                break;
         //            default:
         //                ret = '停用'
         //                break;
         //        }
         //        return ret;
         //    }
         //},
         //{ field: 'Remark', title: '备注', width: 100 },
         //{ field: 'CreatorUserCode', title: '操作员', width: 100 },
         {
             field: 'CreatorUserName', title: '操作员名称', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         },
         {
             field: 'CreationTime', title: '创建时间', width: 100, align: 'center',
             styler: function (value, row, index) {
                 return 'vertical-align:middle;';
             }
         }
         //,{
         //    field: 'LastModificationTime', title: '修改时间', width: 100, align: 'center',
         //    styler: function (value, row, index) {
         //        return 'vertical-align:middle;';
         //    }
         //}
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            $('#WorkState').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZZZT&AllFlag=1',
                onLoadSuccess: function () {
                    $('#WorkState').combobox("setValue", "");
                }
            });

            $('#EmployeeType').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=YGLY&AllFlag=1',
                onLoadSuccess: function () {
                    $('#EmployeeType').combobox("setValue", "");
                }
            });
            //$('#DepartmentCode').combotree({
            //    required: true,
            //    editable: false,
            //    valueField: 'DepartmentCode',
            //    textField: 'DepartmentName',
            //    url: '/PermissionManager/Department/GetList_Combotree'
            //});

            pro.DepartmentControl.init();
            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/EmployeeInfo/Hd", "新增");
            });

            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var EmployeeCode = gridObj.getSelectedRow().EmployeeCode;
                var employeeName = gridObj.getSelectedRow().EmployeeName;
                tabObj.add("/HRManager/EmployeeInfo/Look?PkId=" + PkId + "&EmployeeCode=" + EmployeeCode, "查看(" + employeeName + ")");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var EmployeeCode = gridObj.getSelectedRow().EmployeeCode;
                var employeeName = gridObj.getSelectedRow().EmployeeName;
                tabObj.add("/HRManager/EmployeeInfo/Hd?PkId=" + PkId + "&EmployeeCode=" + EmployeeCode, "编辑(" + employeeName + ")");
            });

            $("#btnUpload").click(function () {
                tabObj.add("/HRManager/EmployeeInfo/Up", "上传");
            });

            $("#btnDown").click(function () {
                $.messager.confirm("确认操作", "是否确认导出", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/HRManager/EmployeeInfo/ExportExcel"
                    }).done(
                    function (dataresult, data) {
                        //
                        if (data && data.success) {
                            location.href = data.targeturl;
                        }
                        //$.alertExtend.info();
                        //gridObj.search();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });

            $("#btnSearch").click(function () {
                gridObj.search();
            });
            $("#btnHistory").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var EmployeeCode = gridObj.getSelectedRow().EmployeeCode;
                tabObj.add("/HRManager/EmployeeInfoHis/List?EmployeeCode=" + EmployeeCode, "历史信息");
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/HRManager/EmployeeInfo/Delete?PkId=" + gridObj.getSelectedRow().PkId
                    }).done(
                    function (dataresult, data) {
                        $.alertExtend.info();
                        gridObj.search();
                    }
                    ).fail(
                    function (errordetails, errormessage) {
                        $.alertExtend.error();
                    }
                    );
                });
            });

            $("#btnRefresh").click(function () {
                gridObj.search();
            });
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.EmployeeInfo.ListPage.initPage();
});



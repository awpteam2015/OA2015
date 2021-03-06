﻿
var pro = pro || {};
(function () {
    pro.HrReport = pro.HrReport || {};
    pro.HrReport.EmployeeZHReport = pro.HrReport.EmployeeZHReport || {};
    pro.HrReport.EmployeeZHReport = {
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
                url: '/ReportManager/HrReport/GetEmployeeZHReport',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'EmployeeCode', title: '员工编号', width: 100 },
         { field: 'EmployeeName', title: '员工名称', width: 100 },
         { field: 'CertNo', title: '身份证', width: 130 },
         {
             field: 'Birthday', title: '生日', width: 100
         },
         {
             field: 'Sex', title: '姓别', width: 100, formatter: function (value, row, index) {
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
         { field: 'EmployeeTypeName', title: '员工类型', width: 100 },
         { field: 'DepartmentName', title: '部门名称', width: 200 },
         { field: 'PostPropertyName', title: '岗位性质', width: 100 },
         { field: 'PostLevelName', title: '岗位等级', width: 100 },
         { field: 'DutiesName', title: '单位职务', width: 100 },
         { field: 'EducationName', title: '学历', width: 100 },
         { field: 'PoliticsName', title: '政治面貌', width: 160 },
         //{
         //    field: 'IsCommy', title: '是否党员', width: 100, formatter: function (value, row, index) {
         //        var ret = "";
         //        switch (value) {
         //            case 0:
         //                ret = '否'
         //                break;
         //            case 1:
         //                ret = '是'
         //                break;
         //            default:
         //                ret = '未知'
         //                break;
         //        }
         //        return ret;
         //    }
         //},
         { field: 'JoinCommy', title: '入党时间', width: 120 }

                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageAttendanceReport: [20, 30, 40] //可以设置每页记录条数的列表    
            },
         { field: 'WorkStateName', title: '在职状态', width: 100 }
               );

            var ReportType = $('#ReportType').val();

            $('#PoliticsName').combobox({
                required: true,
                editable: false,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=ZZMM&AllFlag=1',
                onLoadSuccess: function () {
                    $('#PoliticsName').combobox("setValue", "");
                }
            });
            if (ReportType == '' || ReportType == '3' || ReportType == '4' || ReportType == '5')
                pro.DepartmentControl.init();
            if (ReportType == '' || ReportType == '1')
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
            if (ReportType == '' || ReportType == '2') {
                $('#PostLevel').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=GWDJ&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#PostLevel').combobox("setValue", "");
                    }
                });
                $('#PostProperty').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=GWXZ&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#PostProperty').combobox("setValue", "");
                    }
                });
            }
            if (ReportType == '' || ReportType == '3')
                $('#Duties').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=DWZW&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#Duties').combobox("setValue", "");
                    }
                });
            if (ReportType == '' || ReportType == '5')
                $('#Education').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=Education&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#Education').combobox("setValue", "");
                    }
                });
            if (ReportType == '') {
                $('#LevNum').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=JSZC&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#LevNum').combobox("setValue", "");
                    }
                });
                $('#KHComment').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=NDKH_PJ&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#KHComment').combobox("setValue", "");
                    }
                });
                $('#Degree').combobox({
                    required: true,
                    editable: false,
                    valueField: 'KeyValue',
                    textField: 'KeyName',
                    url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=Degree&AllFlag=1',
                    onLoadSuccess: function () {
                        $('#Degree').combobox("setValue", "");
                    }
                });

            }

            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $('#btnExport').click(function () {
                var urlParam = pro.commonKit.parseParam(gridObj.searchForm());
                // location.href = "/ReportManager/HrReport/ExportEmployeeExcel?" + urlParam;
                $.messager.confirm("确认操作", "是否确认导出", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/ReportManager/HrReport/ExportEmployeeExcel?ReportType=" + $("#ReportType").val()+"&" + urlParam
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

        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.HrReport.EmployeeZHReport.initPage();
});



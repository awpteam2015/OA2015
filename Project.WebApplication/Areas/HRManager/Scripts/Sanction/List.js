
var pro = pro || {};
(function () {
    pro.Sanction = pro.Sanction || {};
    pro.Sanction.ListPage = pro.Sanction.ListPage || {};
    pro.Sanction.ListPage = {
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
                url: '/HRManager/Sanction/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '', hidden: true, width: 100 },
         {
             field: 'SanctionType', title: '奖罚类型', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '奖励'
                         break;
                     case 1:
                         ret = '惩罚'
                         break;
                 }
                 return ret;
             }
         },
          { field: 'SanctionObjLevelName', title: '等级', width: 100 },
         {
             field: 'SanctionObjType', title: '奖罚对象类型', width: 100, formatter: function (value, row, index) {
                 var ret = "";
                 switch (value) {
                     case 0:
                         ret = '个人'
                         break;
                     case 1:
                         ret = '机构（科室）'
                         break;
                 }
                 return ret;
             }
         },
         { field: 'SanctionObjName', title: '奖罚对象', width: 100 },
         { field: 'SanctionTitle', title: '奖罚名目', width: 100 },
         { field: 'SanctionMoney', title: '奖罚金额', width: 100 },
         { field: 'SanctionDate', title: '奖罚日期', width: 100 },
         { field: 'Remark', title: '备注', width: 100 },
         { field: 'CreatorUserCode', title: '操作人', width: 100 },
         { field: 'CreatorUserName', title: '操作人名称', width: 100 },
         { field: 'CreateTime', title: '创建日期', width: 100 },
         //{ field: 'LastModificationTime', title: '修改时间', width: 100 },
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );
            //$('#DepartmentCode').combotree({
            //    required: true,
            //    editable: false,
            //    multiple: true,//支持多选
            //    cascadeCheck: false,
            //    valueField: 'DepartmentCode',
            //    textField: 'DepartmentName',
            //    url: '/PermissionManager/Department/GetList_Combotree',
            //    onLoadSuccess: function (node, data) {
            //        $("#DepartmentCode").combotree('setValue', "0");
            //    }
            //})
            pro.DepartmentControl.init();
            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/Sanction/Hd", "新增");
            });

            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                tabObj.add("/HRManager/Sanction/Hd?PkId=" + PkId, "编辑" + PkId);
            });


            $("#btnSearch").click(function () {
                gridObj.search();
            });

            $("#btnDel").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                $.messager.confirm("确认操作", "是否确认删除", function (bl) {
                    if (!bl) return;
                    abp.ajax({
                        url: "/HRManager/Sanction/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
                gridObj.refresh();
            });
        },
        closeTab: function () {
            this.init().tabObj.closeTab();
        }
    };
})();



$(function () {
    pro.Sanction.ListPage.initPage();
});



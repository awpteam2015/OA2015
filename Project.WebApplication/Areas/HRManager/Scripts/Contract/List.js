
var pro = pro || {};
(function () {
    pro.Contract = pro.Contract || {};
    pro.Contract.ListPage = pro.Contract.ListPage || {};
    pro.Contract.ListPage = {
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
                url: '/HRManager/Contract/GetList',
                fitColumns: false,
                nowrap: false,
                rownumbers: true, //行号
                singleSelect: true,
                columns: [[
         { field: 'PkId', title: '序号', width: 50 },
         { field: 'EmployeeCode', title: '工号', width: 100 },
         { field: 'DepartmentCode', title: '部门编号', width: 100 },
         { field: 'DepartmentName', title: '部门名称', width: 100 },
         { field: 'BeginDate', title: '开始时间', width: 100 },
         { field: 'EndDate', title: '结束时间', width: 100 },
         { field: 'Attr_State', title: '状态 ', width: 100 },
         { field: 'Attr_IsActive', title: '是否有效', width: 100 },
         { field: 'ContractNo', title: '合同编号', width: 100 },
         { field: 'FirstParty', title: '甲方', width: 100 },
         { field: 'SecondParty', title: '乙方', width: 100 },
         { field: 'IdentityCardNo', title: '身份证', width: 100 },
         { field: 'CreatorUserCode', title: '创建人', width: 100 },
         { field: 'CreationTime', title: '创建时间', width: 100 },
         { field: 'LastModifierUserCode', title: '修改人', width: 100 },
         { field: 'LastModificationTime', title: '最后修改时间', width: 100 }
                ]],
                pagination: true,
                pageSize: 20, //每页显示的记录条数，默认为10     
                pageList: [20, 30, 40] //可以设置每页记录条数的列表    
            }
               );

            pro.DepartmentControl.init();

            $("#btnAdd").click(function () {
                tabObj.add("/HRManager/Contract/Hd?State=1&ParentId=0", "新增");
            });

            $("#btnUpload").click(function () {
                tabObj.add("/HRManager/Contract/Upload", "批量上传合同");
            });



            $("#btnAdd2").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var getSelect = gridObj.getSelectedRow();
                if (getSelect.IsActive!=1) {
                    $.alertExtend.infoOp("请选择有效的合同！");
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var secondParty = gridObj.getSelectedRow().SecondParty;
                tabObj.add("/HRManager/Contract/Hd?State=2&Title=续订&ParentId=" + PkId, "续订合同" + secondParty);
            });

            $("#btnAdd3").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var getSelect = gridObj.getSelectedRow();
                if (getSelect.IsActive != 1) {
                    $.alertExtend.infoOp("请选择有效的合同！");
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var secondParty = gridObj.getSelectedRow().SecondParty;
                tabObj.add("/HRManager/Contract/Hd?State=3&Title=变更&ParentId=" + PkId, "变更合同" + secondParty);
            });

            $("#btnAdd4").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var getSelect = gridObj.getSelectedRow();
                if (getSelect.IsActive != 1) {
                    $.alertExtend.infoOp("请选择有效的合同！");
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var secondParty = gridObj.getSelectedRow().SecondParty;
                tabObj.add("/HRManager/Contract/Hd?State=4&Title=终止&ParentId=" + PkId, "终止合同" + secondParty);
            });


            $("#btnEdit").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var secondParty = gridObj.getSelectedRow().SecondParty;
                tabObj.add("/HRManager/Contract/Hd?PkId=" + PkId, "编辑" + secondParty);
            });

            $("#btnView").click(function () {
                if (!gridObj.isSelected()) {
                    $.alertExtend.infoOp();
                    return;
                }
                var PkId = gridObj.getSelectedRow().PkId;
                var secondParty = gridObj.getSelectedRow().SecondParty;
                tabObj.add("/HRManager/Contract/Hd?&Title=查看&PkId=" + PkId, "查看" + secondParty);
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
                        url: "/HRManager/Contract/Delete?PkId=" + gridObj.getSelectedRow().PkId
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
    pro.Contract.ListPage.initPage();
});



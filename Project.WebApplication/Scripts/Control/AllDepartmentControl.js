var pro = pro || {};
(function () {
    pro.AllDepartmentControl = pro.AllDepartmentControl || {};
    pro.AllDepartmentControl = {
        init: function (paramter) {
            var defaultParamter = {
                controlId: "DepartmentCode",
                editable: false,
                width:300,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetAllist_Combotree'
            };

            var options = $.extend({}, defaultParamter, paramter);
            $('#' + options.controlId).combotree(options);

        }
    }
}
)();


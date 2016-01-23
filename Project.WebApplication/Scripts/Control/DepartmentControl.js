﻿var pro = pro || {};
(function () {
    pro.DepartmentControl = pro.DepartmentControl || {};
    pro.DepartmentControl = {
        init: function (paramter) {
            var defaultParamter = {
                controlId: "DepartmentCode",
                required: true,
                editable: false,
                valueField: 'DepartmentCode',
                textField: 'DepartmentName',
                url: '/PermissionManager/Department/GetList_Combotree'
            };

            var options = $.extend({}, defaultParamter, paramter);
            $('#' + options.controlId).combotree(options);

        }
    }
}
)();


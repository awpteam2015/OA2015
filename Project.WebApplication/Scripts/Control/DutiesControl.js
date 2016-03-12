var pro = pro || {};
(function () {
    pro.DutiesControl = pro.DutiesControl || {};
    pro.DutiesControl = {
        init: function (paramter) {
            var defaultParamter = {
                controlId: "Duties",
                editable: false,
                width: 300,
                valueField: 'KeyValue',
                textField: 'KeyName',
                url: '/HRManager/Dictionary/GetListByCode?ParentKeyCode=DWZW'
            };

            var options = $.extend({}, defaultParamter, paramter);
            $('#' + options.controlId).combobox(options);

        }
    }
}
)();

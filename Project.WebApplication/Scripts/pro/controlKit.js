﻿var pro = pro || {};
(function () {
    pro.controlKit = pro.controlKit || {};
    pro.controlKit = {
        //获取Input Span的格式
        getInputAndSpanHtml: function (name, value) {
            if (value == undefined) {
                value = "";
            }
            var html = '<input name="' + name + '" value="' + value + '"  type="text" style="display:none"/>';
            var name = name.split('_');
            html += ' <span name="' + name[0] + "_span_" + name[1] + '"  >' + value + '</span>';
            return html;
        },
        //获取Input的格式
        getInputHtml: function (name, value,width) {
            if (value == undefined) {
                value = "";
            }

            if (width == undefined) {
                width = "100";
            }

            var html = '<input name="' + name + '" value="' + value + '"  type="text" style="width:' + width + 'px" />';
            return html;
        },
        //获取Input的格式
        getInputDateHtml: function (name, value) {
            if (value == undefined) {
                value = "";
            }
            var html = '<input class="Wdate"   name="' + name + '" value="' + value + '"  onclick="WdatePicker({skin:\'ext\'});"  type="text"/>';
            return html;
        },
        //获取Span的格式
        getSpanHtml: function (name, value) {
            if (value == undefined) {
                value = "";
            }
            var name = name.split('_');
            var html = ' <span name="' + name[0] + "_span_" + name[1] + '"  >' + value + '</span>';
            return html;
        },
        //获取input的格式
        getSelectHtml: function (name, value, html) {
            if (value == undefined) {
                value = "";
            }
            var html = '<select name="' + name + '">' + html + '</select>';
            if (value) {
                html += '<script>$("select[name=' + name + ']").val("' + value + '");</script>';
            }
            return html;
        },
        //给input和span赋值
        setInputAndSpanValue: function (name, value) {
            if (value == undefined) {
                value = "";
            }
            $("input[name=" + name + "]").val(value);
            var name = name.split('_');
            if (name.length == 2) {
                $("span[name=" + name[0] + "_span_" + name[1] + "]").html(value);
            }
            else {
                $("span[name=" + name[0] + "_span]").html(value);
            }
        },
        //给span赋值
        setSpanValue: function (name, value) {
            if (value == undefined) {
                value = "";
            }
            var name = name.split('_');
            if (name.length == 2) {
                $("span[name=" + name[0] + "_span_" + name[1] + "]").html(value);
            }
            else {
                $("span[name=" + name[0] + "_span]").html(value);
            }
        },
        //给input赋值
        setInputValue: function (name, value) {
            if (value == undefined) {
                value = "";
            }
            $("input[name=" + name + "]").val(value);
        }
    };
})();
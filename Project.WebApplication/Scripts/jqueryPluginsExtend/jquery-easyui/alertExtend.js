
$.alertExtend = $.alertExtend || {};
(function () {
    $.alertExtend = {
        error: function (msg, fn) {
            ///<summary>
            ///错误提示
            ///</summary>
            ///<param name="msg" type="String">提示信息</param>
            ///<param name="fn" type="function">关闭之后回调函数</param>
            if (msg == undefined || msg==="") {
                msg = "操作失败！";
            }
            if (fn) $.messager.alert("错误提示", msg, "error", fn);
            else $.messager.alert("错误提示", msg, "error");
        },
        question: function (msg, fn) {
            ///<summary>
            ///问题提示
            ///</summary>
            ///<param name="msg" type="String">提示信息</param>
            ///<param name="fn" type="function">关闭之后回调函数</param>
            if (fn) $.messager.alert("问题提示", msg, "question", fn);
            else $.messager.alert("问题提示", msg, "question");
        },
        info: function (msg, fn) {
            ///<summary>
            ///信息提示
            ///</summary>
            ///<param name="msg" type="String">提示信息</param>
            ///<param name="fn" type="function">关闭之后回调函数</param>
            if (msg == undefined || msg === "") {
                msg = "操作成功！";
            }
            if (fn) $.messager.alert("信息提示", msg, "info", fn);
            else $.messager.alert("信息提示", msg, "info");
        },
        infoOp: function (msg, fn) {
            if (msg == undefined || msg === "") {
                msg = "请选择需要操作的行项目！";
            }
            this.info(msg, fn);
        },
        warning: function (msg, fn) {
            ///<summary>
            ///警告提示
            ///</summary>
            ///<param name="msg" type="String">提示信息</param>
            ///<param name="fn" type="function">关闭之后回调函数</param>
            if (fn) $.messager.alert("提示", msg, "warning", fn);
            else $.messager.alert("提示", msg, "warning");
        }
    };
})();



var pro = pro || {};
(function () {
    pro.LoginPage = pro.LoginPage || {};
    pro.LoginPage = {
        initPage: function () {
            $("#btn_Login2").click(function () {
                pro.LoginPage.Login();
            });
        },
        Login: function () {
            var postData = {};
            postData.userCode = "";
            postData.password = "";
            abp.ajax({
                url: "/Account/UserLogin",
                data: '{"userCode":"","password":""}'
            }).done(
                function (data, data2) {
                    window.location.href = "/Account/Index";
                }
            );
        }
    };
})();


$(function () {
    pro.LoginPage.initPage();
});


var pro = pro || {};
(function () {
    pro.LoginPage = pro.LoginPage || {};
    pro.LoginPage = {
        initPage: function () {
            $("#btn_Login").click(function () {
                pro.LoginPage.Login();
            });
        },
        Login: function () {
            var postData = pro.submitKit.getHeadJson();
            abp.ajax({
                url: "/Login/UserLogin",
                data: JSON.stringify(postData)
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


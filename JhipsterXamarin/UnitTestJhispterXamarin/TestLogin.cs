using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;

namespace UnitTestJhispterXamarin
{
    [TestClass]
    public class TestLogin : MvxIoCSupportingTest
    {
        LoginViewModel loginViewModel;
        LoginModel loginModel = new LoginModel();

        [TestMethod]
        public void TestViewModel()
        {
            initializeAll();
        }
        public void initializeAll()
        {
            base.Setup(); // from MvxIoCSupportingTest
            AdditionalSetup();
            loginViewModel.Initialize();
        }
        protected override void AdditionalSetup()
        {
            var mockAuthService = new Mock<IAuthenticationService>();
            var mockNavLoginService = new Mock<IMvxNavigationService>();
            loginViewModel = new LoginViewModel(mockNavLoginService.Object, mockAuthService.Object);

            Ioc.RegisterSingleton<IAuthenticationService>(mockAuthService.Object);
            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavLoginService.Object);
        }
        [TestMethod]
        public void TestSignInAdmin()
        {
            initializeAll();

            loginModel.Username = "admin";
            loginModel.Password = "admin";
            loginModel.RememberMe = false;

            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;

            Assert.AreEqual(loginViewModel.GetAuthenticationService().SignIn(loginModel).ToString(), loginViewModel.SignInConnection().ToString(), "Test failed : login error user : admin  | password : admin");
        }
        [TestMethod]
        public void TestSignOutAdmin()
        {
            initializeAll();
            loginModel.Username = "admin";
            loginModel.Password = "admin";
            loginModel.RememberMe = false;
            loginViewModel.GetAuthenticationService().SignIn(loginModel);
            var result = loginViewModel.GetAuthenticationService().SignOut();
            Assert.AreEqual(null,result , "Test failed : logout error");
        }
    }
}

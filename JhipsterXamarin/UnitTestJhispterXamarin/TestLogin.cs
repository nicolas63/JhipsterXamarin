using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using FluentAssertions;


namespace UnitTestJhispterXamarin
{
    [TestClass]
    public class TestLogin : MvxIoCSupportingTest
    {
        LoginViewModel loginViewModel;
        LoginModel loginModel = new LoginModel();

        [TestInitialize]
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
            loginViewModel.GetAuthenticationService().SignIn(loginModel).ToString()
                .Should().Be(loginViewModel.SignInConnection().ToString(), "Test failed because of a bad move to login the admin");
 
        }
        [TestMethod]
        public void TestSignOutAdmin()
        {
            initializeAll();
            loginModel.Username = "admin";
            loginModel.Password = "admin";
            loginModel.RememberMe = false;
            loginViewModel.GetAuthenticationService().SignIn(loginModel);
            loginViewModel.GetAuthenticationService().SignOut().Should().BeNull("Test failed because of a bad move to logout the admin");
        }
    }
}

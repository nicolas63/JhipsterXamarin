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
        public void Initialize()
        {
            base.Setup(); // from MvxIoCSupportingTest
            var mockAuthService = new Mock<IAuthenticationService>();
            var mockNavLoginService = new Mock<IMvxNavigationService>();
            loginViewModel = new LoginViewModel(mockNavLoginService.Object, mockAuthService.Object);

            Ioc.RegisterSingleton<IAuthenticationService>(mockAuthService.Object);
            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavLoginService.Object);
        }
        [TestMethod]
        public void TestSignInAdmin()
        {
            //Arrange
            loginModel.Username = "admin";
            loginModel.Password = "admin";
            loginModel.RememberMe = false;

            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;
            //Act
            var result = loginViewModel.SignInConnection().ToString();
            //Assert
            loginViewModel.GetAuthenticationService().SignIn(loginModel).ToString()
                .Should().Be(result, "Test failed because of a bad move to login the admin");
        }
        [TestMethod]
        public void TestSignOutAdmin()
        {
            //Arrange
            loginModel.Username = "admin";
            loginModel.Password = "admin";
            loginModel.RememberMe = false;
            //Act
            loginViewModel.GetAuthenticationService().SignIn(loginModel);
            //Assert
            loginViewModel.GetAuthenticationService().SignOut().Should().BeFalse("Test failed because of a bad move to logout the admin");
        }
    }
}

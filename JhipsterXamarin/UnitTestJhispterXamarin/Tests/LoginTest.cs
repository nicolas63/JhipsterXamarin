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
    public class LoginTest : MvxIoCSupportingTest
    {
         private LoginViewModel loginViewModel;

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
            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;

            //Act
            var result = loginViewModel.SignInConnection().ToString();

            //Assert
            result
                .Should().NotBeNullOrEmpty("Test failed because of a bad move to login the admin");
        }
    }
}

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
        public void Should_SignInAdmin_When_SignInButtonClicked()
        {   
            //Arrange
            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;

            //Act
            var signInResult = loginViewModel.SignIn;

            //Assert
            signInResult
                .Should().NotBe(null, "Test failed because of a bad move to login the admin");

            loginViewModel.Active
                .Should().BeTrue("Test failed because of a bad move to login the admin");

            loginViewModel.Success
                .Should().BeTrue("Test failed because of a bad move to login the admin");
        }

        [TestMethod]
        public void Should_SignInAdmin_When_SignInConnectionCommandAsked()
        {
            //Arrange
            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;

            //Act
            var signInConnectionResult = loginViewModel.SignInConnection().ToString(); ;

            //Assert
            signInConnectionResult
                .Should().NotBeNullOrEmpty("Test failed because of a bad move to login the admin");

            loginViewModel.Active
                .Should().BeTrue("Test failed because of a bad move to login the admin");

            loginViewModel.Success
                .Should().BeTrue("Test failed because of a bad move to login the admin");
        }

        [TestMethod]
        public void Should_ReloadAdmin_When_ReloadCommandAsked()
        {
            //Arrange
            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;

            //Act
            loginViewModel.ReloadActive();
            var activeResult = loginViewModel.Active;

            //Assert
            activeResult
                .Should().BeTrue("Test failed because of a bad move to login the admin");

            loginViewModel.Success
                .Should().BeTrue("Test failed because of a bad move to login the admin");

        }

        [TestMethod]
        public void Should_ChangeStateRemember_When_ChangeStateCommandAsked()
        {
            
            //Arrange
            loginViewModel.Username = "admin";
            loginViewModel.Password = "admin";
            loginViewModel.RememberMe = false;

            //Act
            var changeStateResult = loginViewModel.ChangeStateCommand;

            //Assert
            changeStateResult
                .Should().NotBe(null, "Test failed because of a bad move to login the admin");

        }

        [TestMethod]
        public void Should_SignUp_When_SignUpButtonClicked()
        {
            //Arrange

            //Act
            var result = loginViewModel.SignUp;

            //Assert
            result
                .Should().NotBe(null, "Test failed because of a bad move to login the admin");
        }
    }
}

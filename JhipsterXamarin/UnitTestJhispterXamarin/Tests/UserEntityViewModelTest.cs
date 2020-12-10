using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using JhipsterXamarin.Constants;
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
    public class UserEntityViewModelTest : MvxIoCSupportingTest
    {
        private readonly List<string> listAuthorities = new List<string>()
        {
            RolesConstants.ADMIN,
            RolesConstants.ANONYMOUS,
            RolesConstants.USER
        };

        private readonly Fixture _fixture = new Fixture();
        private readonly List<UserModel> _users = new List<UserModel>();
        private readonly Mock<IMvxNavigationService> _mockNavLoginService = new Mock<IMvxNavigationService>();
        private readonly Mock<IAuthenticationService> _mockAuthLoginService = new Mock<IAuthenticationService>();
        private readonly Mock<IUserEntityService<UserModel>> _mockUserEntityService
            = new Mock<IUserEntityService<UserModel>>();

        private UserEntityViewModel userEntityViewModel;

        [TestInitialize]
        public async Task Initialize()
        {
            base.Setup(); // from MvxIoCSupportingTest

            Ioc.RegisterSingleton<IUserEntityService<UserModel>>(_mockUserEntityService.Object);
            Ioc.RegisterSingleton<IMvxNavigationService>(_mockNavLoginService.Object);
            Ioc.RegisterSingleton<IAuthenticationService>(_mockAuthLoginService.Object);

            userEntityViewModel = new UserEntityViewModel(_mockNavLoginService.Object, _mockUserEntityService.Object, _mockAuthLoginService.Object);

            await userEntityViewModel.Initialize();
            userEntityViewModel.UserModels = _users;
        }

        private UserModel GenerateUser(string firstName, string lastName)
        {
            var authorities = new List<string>()
            {
                listAuthorities[0]
            };

            var user = _fixture.Create<UserModel>();

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = firstName+lastName+"@localhost";
            user.Activated = true;
            user.Authorities = authorities;


            userEntityViewModel.FirstName = user.FirstName;
            userEntityViewModel.LastName = user.LastName;
            userEntityViewModel.Email = user.Email;
            userEntityViewModel.CurrentRole = listAuthorities[0];

            return user;

        }

        [TestMethod]
        public void Should_AddUser_When_AddCommandClickedAsked()
        {
            //Arrange
            var user = GenerateUser("test", "testUser");

            //Act
            var addCommandClickedResult =  userEntityViewModel.AddCommandClicked();

            //Assert
            addCommandClickedResult
                .Should().NotBe(null,"Test failed because of a bad move");

        }

        [TestMethod]
        public void Should_AddUser_When_AddCommandAsked()
        {
            //Arrange
            var user = GenerateUser("test", "testUser");

            //Act
            var addCommandResult = userEntityViewModel.AddCommand;

            //Assert
            addCommandResult
                .Should().NotBe(null, "Test failed because of a bad move");

        }

        [TestMethod]
        public void Should_RemoveUser_When_RemoveCommandClicked()
        {
            //Arrange
            GenerateUser("first", "firstTestUser");
            userEntityViewModel.AddCommandClicked();

            GenerateUser("second", "secondTestUser");
            userEntityViewModel.AddCommandClicked();

            //Act
            var removeCommandClickedResult = userEntityViewModel.RemoveCommandClicked();

            //Assert
            removeCommandClickedResult
                .Should().NotBe(null, "Test failed because of a bad move");

        }

        [TestMethod]
        public void Should_RemoveUser_When_RemoveCommandAsked()
        {
            //Arrange
            GenerateUser("first", "firstTestUser");
            userEntityViewModel.AddCommandClicked();
            
            GenerateUser("second", "secondTestUser");
            userEntityViewModel.AddCommandClicked();

            //Act
            var removeCommandResult = userEntityViewModel.RemoveCommand;

            //Assert
            removeCommandResult
                .Should().NotBe(null, "Test failed because of a bad move");
        }

        [TestMethod]
        public void Should_EditUser_When_EditCommandClicked()
        {
            //Arrange
            GenerateUser("first", "firstTestUser");

            userEntityViewModel.AddCommandClicked();

            var newFirstName = "firstUpdated";
            var newLastName = "firstTestUserUpdated";

            userEntityViewModel.FirstName = newFirstName;
            userEntityViewModel.LastName = newLastName;

            //Act
            var editCommandClickedResult = userEntityViewModel.EditCommandClicked();

            //Assert
            editCommandClickedResult
                .Should().NotBe(null, "Test failed because of a bad move");
        }

        [TestMethod]
        public void Should_EditUser_When_EditCommandAsked()
        {
            //Arrange
            GenerateUser("first", "firstTestUser");

            userEntityViewModel.AddCommandClicked();

            var newFirstName = "firstUpdated";
            var newLastName = "firstTestUserUpdated";

            userEntityViewModel.FirstName = newFirstName;
            userEntityViewModel.LastName = newLastName;

            //Act
            var editCommandClickedResult = userEntityViewModel.EditCommand;

            //Assert
            editCommandClickedResult
                .Should().NotBe(null, "Test failed because of a bad move");

        }

        [TestMethod]
        public void Should_ShowUserData_When_UserSelected()
        {
            //Arrange
            var user =GenerateUser("first", "firstTestUser");
            
            userEntityViewModel.UserModels.Add(user);
            userEntityViewModel.CurrentElement = userEntityViewModel.UserModels[0];

            //Act
            var currentUser = userEntityViewModel.CurrentElement;

            //Assert
            currentUser
                .Should().Be(user, "Test failed because of a bad move");
        }

    }
}

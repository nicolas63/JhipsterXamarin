﻿using FluentAssertions;
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
        private UserEntityViewModel userEntityViewModel;

        private UserModel admin;
        private UserModel paul;
        private UserModel john;

        [TestInitialize]
        public void Initialize()
        {
            base.Setup(); // from MvxIoCSupportingTest

            var mockUserEntityService = new Mock<UserEntityService<UserModel>>();
            var mockNavLoginService = new Mock<IMvxNavigationService>();

            userEntityViewModel = new UserEntityViewModel(mockNavLoginService.Object, mockUserEntityService.Object);

            Ioc.RegisterSingleton<UserEntityService<UserModel>>(mockUserEntityService.Object);
            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavLoginService.Object);
        }

        protected override void AdditionalSetup()
        {
            admin = new UserModel();
            admin.Id = "admin";
            admin.Login = "admin";

            paul = new UserModel();
            paul.Id = "paul";
            paul.Login = "paul";

            john = new UserModel();
            john.Id = "john";
            john.Login = "john";
        }

        [TestMethod]
        public void TestSetAdminAsync()
        {
            //Arrange
            userEntityViewModel.SetCurrentUserAsync(admin).Wait();

            //Act
            var result = userEntityViewModel.GetCurrentUser().Login;

            //Assert
            result.Should().NotBeNullOrEmpty("Test failed because of a bad move to login the admin");
        }

        [TestMethod]
        public void TestAddUsers()
        {
            //Arrange
            userEntityViewModel.SetCurrentUserAsync(admin).Wait();

            //Act
            userEntityViewModel.UserModels.Add(paul);
            userEntityViewModel.UserModels.Add(john);

            var result = userEntityViewModel.UserModels.Count;

            //Assert
            result.Should().Be(2,"Test failed because of a bad move to login the admin");
        }

        public void TestRemoveUsers()
        {
            //Arrange
            userEntityViewModel.SetCurrentUserAsync(admin).Wait();

            userEntityViewModel.UserModels.Add(paul);
            userEntityViewModel.UserModels.Add(john);

            //Act
            userEntityViewModel.UserModels.Remove(john);
            var result = userEntityViewModel.UserModels.Count;

            //Assert
            result.Should().Be(1, "Test failed because of a bad move to login the admin");
        }
    }
}

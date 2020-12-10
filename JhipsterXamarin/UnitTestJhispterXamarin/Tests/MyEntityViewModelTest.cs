using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoFixture;

namespace UnitTestJhispterXamarin
{
    [TestClass]
    public class MyEntityViewModelTest : MvxIoCSupportingTest
    {
        private readonly Mock<IMyEntityService> _mockMyEntityService = new Mock<IMyEntityService>();
        private readonly List<MyEntityModel> _myEntities = new List<MyEntityModel>();
        private readonly Fixture _fixture = new Fixture();

        private MyEntityViewModel _myEntityViewModel;

        [TestInitialize]
        public async Task Initialize()
        {
            var mockNavService = new Mock<IMvxNavigationService>();

            base.Setup(); // from MvxIoCSupportingTest 

            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavService.Object);
            Ioc.RegisterSingleton<IMyEntityService>(_mockMyEntityService.Object);

            _myEntityViewModel = new MyEntityViewModel(mockNavService.Object, _mockMyEntityService.Object);
            await _myEntityViewModel.Initialize();

            _myEntityViewModel.ListElement = _myEntities;

        }

        [TestMethod]
        public void Should_CallTheServiceForAddCommand_When_AddCommandAsked()
        {
            //Arrange
            var myEntity = _fixture.Create<MyEntityModel>();

            _myEntityViewModel.Name = myEntity.Name;
            _myEntityViewModel.Age = myEntity.Age;

            //Act
            var result = _myEntityViewModel.AddCommand;
            var addCommandClickedResult = _myEntityViewModel.AddCommandClicked();

            //Assert
            result
                .Should().NotBe(null, "Test failed: We wanted to call the service: ");

            addCommandClickedResult
                .Should().NotBe(null, "Test failed: We wanted to call the service: ");

        }

        [TestMethod]
        public void Should_CallTheServiceForRemoveCommand_When_RemoveCommandAsked()
        {
            //Arrange
            var myEntity = _fixture.Create<MyEntityModel>();

            _myEntityViewModel.ListElement.Add(myEntity);

            //Act
            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            var removeCommandResult = _myEntityViewModel.RemoveCommand;
            var removeCommandClickedResult =  _myEntityViewModel.RemoveCommandClicked();

            //Assert
            removeCommandResult
                .Should().NotBe(null, "Test failed: We wanted to call the service: ");

            removeCommandClickedResult
                .Should().NotBe(null, "Test failed: We wanted to call the service: ");

        }

        [TestMethod]
        public void Should_CallTheServiceForEditCommand_When_EditCommandAsked()
        {
            //Arrange
            var myEntity = _fixture.Create<MyEntityModel>();

            _myEntityViewModel.Name = "the first entity updated";

            _myEntityViewModel.ListElement.Add(myEntity);

            //Act
            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            var editCommandResult = _myEntityViewModel.EditCommand;

            //Assert
            editCommandResult
                .Should().NotBe(null, "Test failed: We wanted to call the service: ");

        }

        [TestMethod]
        public void Should_UpdateTheFirstEntity_When_NameOfFirstEntityChanged()
        {
            //Arrange
            var myEntity = _fixture.Create<MyEntityModel>();
            _myEntityViewModel.ListElement.Add(myEntity);

            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            var expected = "the first entity updated";
            _myEntityViewModel.Name = expected;

            //Act
            _myEntityViewModel.EditCommandClicked().Wait();

            var result = myEntity.Name;

            //Assert
            result.Should().Be(expected, "Test failed: We wanted : " + expected);

        }

        [TestMethod]
        public void Should_DeleteTheFirstEntity_When_DeleteButtonClicked()
        {
            //Arrange
            var myEntity = _fixture.Create<MyEntityModel>();
            var mySecondEntity = _fixture.Create<MyEntityModel>();

            _myEntityViewModel.ListElement.Add(myEntity);
            _myEntityViewModel.ListElement.Add(mySecondEntity);

            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            //Act
            _myEntityViewModel.ListElement.Remove(_myEntityViewModel.CurrentElement);

            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            //Assert
            _myEntityViewModel.ListElement.Count
                .Should().Be(1, "Test Failed : We wanted : " + 1);

            _myEntityViewModel.CurrentElement
                .Should().Be(mySecondEntity, "Test Failed : we wanted : " + mySecondEntity.ToString());

        }

        [TestMethod]
        public void Should_AddNewEntity_When_AddCommandClicked()
        {
            //Arrange
            var myEntity = _fixture.Create<MyEntityModel>();
            var mySecondEntity = _fixture.Create<MyEntityModel>();

            List<MyEntityModel> listEntityModels = new List<MyEntityModel>
            {
                myEntity
            };
            _myEntityViewModel.ListElement = listEntityModels;

            //Act
            _myEntityViewModel.ListElement.Add(mySecondEntity);
            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[1];

            //Assert
            _myEntityViewModel.ListElement.Count
                .Should().Be(2, "Test Failed : We wanted : " + 2);

            _myEntityViewModel.CurrentElement
                .Should().Be(mySecondEntity, "Test Failed : we wanted : " + mySecondEntity);

        }
    }
}

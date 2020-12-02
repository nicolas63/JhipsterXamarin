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
        public void Should_UpdateTheFirstEntity_When_NameOfFirstEntityChange()
        {
            //Arrange
            var sut = _fixture.Create<MyEntityModel>();
            _myEntityViewModel.ListElement.Add(sut);

            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            var expected = "the first entity updated";
            _myEntityViewModel.Name = expected;

            //Act
            _myEntityViewModel.EditCommandClicked().Wait();

            var result = sut.Name;

            //Assert
            result.Should().Be(expected, "Test failed: We wanted : " + expected);

        }

        [TestMethod]
        public void Should_DeleteTheFirstEntity_When_DeleteButtonClicked()
        {
            //Arrange
            var sut = _fixture.Create<MyEntityModel>();
            var sut2 = _fixture.Create<MyEntityModel>();

            _myEntityViewModel.ListElement.Add(sut);
            _myEntityViewModel.ListElement.Add(sut2);

            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            //Act
            _myEntityViewModel.ListElement.Remove(_myEntityViewModel.CurrentElement);

            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[0];

            //Assert
            _myEntityViewModel.ListElement.Count
                .Should().Be(1, "Test Failed : We wanted : " + 1);

            _myEntityViewModel.CurrentElement
                .Should().Be(sut2, "Test Failed : we wanted : " + sut2.ToString());

        }
        [TestMethod]
        public void Should_AddNewEntity_When_AddCommandClicked()
        {
            //Arrange
            var sut = _fixture.Create<MyEntityModel>();
            var sut2 = _fixture.Create<MyEntityModel>();

            List<MyEntityModel> listEntityModels = new List<MyEntityModel>
            {
                sut
            };
            _myEntityViewModel.ListElement = listEntityModels;

            //Act
            _myEntityViewModel.ListElement.Add(sut2);
            _myEntityViewModel.CurrentElement = _myEntityViewModel.ListElement[1];

            //Assert
            _myEntityViewModel.ListElement.Count
                .Should().Be(2, "Test Failed : We wanted : " + 2);

            _myEntityViewModel.CurrentElement
                .Should().Be(sut2, "Test Failed : we wanted : " + sut2);

        }
    }
}

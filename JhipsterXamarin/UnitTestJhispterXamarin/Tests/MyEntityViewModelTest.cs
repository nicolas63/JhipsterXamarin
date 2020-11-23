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

namespace UnitTestJhispterXamarin
{
    [TestClass]
    public class MyEntityViewModelTest : MvxIoCSupportingTest
    {
        private Mock<IMyEntityService> myEntityService;
        private List<MyEntityModel> myEntities;
        private MyEntityViewModel myEntityViewModel;        

        private readonly MyEntityModelSimple myFirstEntityModelSimple = new MyEntityModel();
        private readonly MyEntityModelSimple mySecondEntityModelSimple = new MyEntityModel();

        [TestInitialize]
        public async Task Initialize()
        {
            myEntityService = new Mock<IMyEntityService>();
            var mockNavService = new Mock<IMvxNavigationService>();

            base.Setup(); // from MvxIoCSupportingTest 

            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavService.Object);
            Ioc.RegisterSingleton<IMyEntityService>(myEntityService.Object);

            myEntityViewModel = new MyEntityViewModel(mockNavService.Object, myEntityService.Object);
            await myEntityViewModel.Initialize();

            myEntityViewModel.ListElement = myEntities;
        }
        protected override void AdditionalSetup()
        {
            myFirstEntityModelSimple.Name = "the first entity";
            myFirstEntityModelSimple.Age = 1;

            mySecondEntityModelSimple.Name = "the second entity";
            mySecondEntityModelSimple.Age = 2;

            MyEntityModelSimple myThirdEntityModelSimple = new MyEntityModel();
            myThirdEntityModelSimple.Name = "the third entity";
            myThirdEntityModelSimple.Age = 3;

            myEntities = new List<MyEntityModel>();

            myEntities.Add((MyEntityModel)myFirstEntityModelSimple);
            myEntities.Add((MyEntityModel)mySecondEntityModelSimple);
            myEntities.Add((MyEntityModel)myThirdEntityModelSimple);
        }

        [TestMethod]
        public void Should_UpdateTheFirstEntity_When_NameOfFirstEntityChange()
        {
            //Arrange
            string expected = "the first entity updated";
            myFirstEntityModelSimple.Name = expected;

            //Act
            var listElement = myEntityViewModel.ListElement;
            if (listElement.Contains((MyEntityModel)myFirstEntityModelSimple))
                myEntityViewModel.CurrentElement = (MyEntityModel)myFirstEntityModelSimple;

            //Assert
            myEntityViewModel.CurrentElement.Name
                .Should().Be(expected, "Test failed: We wanted : " + expected);
        }

        [TestMethod]
        public void Should_DeleteTheFirstEntity_When_DeleteButtonClicked()
        {
            //Arrange
            var listElement = myEntityViewModel.ListElement;

            //Act
            if (listElement.Contains((MyEntityModel)myFirstEntityModelSimple))
                myEntityViewModel.ListElement.Remove((MyEntityModel)myFirstEntityModelSimple);
                myEntityViewModel.CurrentElement = myEntityViewModel.ListElement[0];

            //Assert
            myEntityViewModel.ListElement.Count
                .Should().Be(2, "Test Failed : We wanted : " + 2);
            myEntityViewModel.CurrentElement.Should().Be(mySecondEntityModelSimple, "Test Failed : we wanted : " + mySecondEntityModelSimple.ToString());
        }
    }
}

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
    public class TestService : MvxIoCSupportingTest
    {
        Mock<IMyEntityService> myEntityService;
        MyEntityViewModel myEntityViewModel;
        MyEntityModelSimple myFirstEntityModelSimple = new MyEntityModel();

        [TestInitialize]
        public async Task Initialize()
        {
            myEntityService = new Mock<IMyEntityService>();
            var mockNavService = new Mock<IMvxNavigationService>();
            base.Setup(); // from MvxIoCSupportingTest 

            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavService.Object);
            Ioc.RegisterSingleton<IMyEntityService>(myEntityService.Object);
            myEntityViewModel = new MyEntityViewModel(mockNavService.Object, myEntityService.Object);
            myEntityService.Object.CreateEntity(myFirstEntityModelSimple.Name, myFirstEntityModelSimple.Age);
            _ = myEntityViewModel.AddCommand;
            await myEntityViewModel.Initialize();
            var list = myEntityViewModel.ListElement;
            var listCount = list.Count;
        }
        protected override void AdditionalSetup()
        {
            myFirstEntityModelSimple.Name = "the first entity";
            myFirstEntityModelSimple.Age = 1;
            myEntityService.Object.CreateEntity(myFirstEntityModelSimple.Name, myFirstEntityModelSimple.Age);
           /* myEntityService.Verify(foo => foo.CreateEntity(myFirstEntityModelSimple.Name, myFirstEntityModelSimple.Age));
            myEntityService.Verify(foo => foo.CreateEntity("the second entity", 2));
            myEntityService.Verify(foo => foo.CreateEntity("the third entity", 3));*/
        }

        [TestMethod]
        public void Should_UpdateTheFirstEntity_When_NameOfFirstEntityChange()
        {
            //Arrange
            string expected = "the first entity updated";
            myFirstEntityModelSimple.Name = expected;
            //Act
            myEntityService.Setup(foo => foo.UpdateEntity((MyEntityModel)myFirstEntityModelSimple));
            var listElement = myEntityViewModel.ListElement;
            if (listElement.Contains((MyEntityModel)myFirstEntityModelSimple))
                myEntityViewModel.CurrentElement = (MyEntityModel)myFirstEntityModelSimple;
            //Assert
            myEntityViewModel.CurrentElement.Name
                .Should().Be(expected, "Test failed: We wanted : " + expected + " We have :  " + myEntityViewModel.CurrentElement.Name);
        }

        [TestMethod]
        public void Should_DeleteTheFirstEntity_When_DeleteButtonClicked()
        {
            //Act
            myEntityService.Setup(foo => foo.DeleteEntity((MyEntityModel)myFirstEntityModelSimple)); 
            //Assert
            myEntityViewModel.ListElement.Count
                .Should().Be(2, "Test Failed : We wanted : " + 2 + " We have :  " + myEntityViewModel.ListElement.Count);
        }
    }
}

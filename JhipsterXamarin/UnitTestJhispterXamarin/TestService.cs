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
    public class TestService : MvxIoCSupportingTest
    {
        Mock<IMyEntityService> myEntityService;
        MyEntityViewModel myEntityViewModel;
        MyEntityModelSimple myFirstEntityModelSimple = new MyEntityModel();

        [TestInitialize]
        public void Initialize()
        {
            base.Setup(); // from MvxIoCSupportingTest

            myEntityService = new Mock<IMyEntityService>();
            var mockNavService = new Mock<IMvxNavigationService>();
            myEntityViewModel = new MyEntityViewModel(mockNavService.Object, myEntityService.Object);

            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavService.Object);
            Ioc.RegisterSingleton<IMyEntityService>(myEntityService.Object);        

            AdditionalSetup();
        }
        protected override void AdditionalSetup()
        {
            myFirstEntityModelSimple.Name = "the first entity";
            myFirstEntityModelSimple.Age = 1;
            myEntityService.Setup(foo => foo.CreateEntity(myFirstEntityModelSimple.Name, myFirstEntityModelSimple.Age));
            myEntityService.Setup(foo => foo.CreateEntity("the second entity", 2));
            myEntityService.Setup(foo => foo.CreateEntity("the third entity", 3));
            myEntityService.Setup(foo => foo.CreateEntity("the third entity", 3));
            myEntityViewModel.Initialize();
        }

        [TestMethod]
        public void Should_UpdateTheFirstEntity_When_NameOfFirstEntityChange()
        {
            //Arrange
            string expected = "the first entity updated";
            myFirstEntityModelSimple.Name = expected;
            //Act
            myEntityService.Verify(foo => foo.UpdateEntity((MyEntityModel)myFirstEntityModelSimple));
            //Assert
            myEntityViewModel.CurrentElement.Name
                .Should().Be(expected, "Test failed: We wanted : " + expected + " We have :  " + myEntityViewModel.CurrentElement.Name);
        }

        [TestMethod]
        public void Should_DeleteTheFirstEntity_When_DeleteButtonClicked()
        {
            //Act
            myEntityService.Verify(foo => foo.DeleteEntity((MyEntityModel)myFirstEntityModelSimple)); 
            //Assert
            myEntityViewModel.ListElement.Count
                .Should().Be(2, "Test Failed : We wanted : " + 2 + " We have :  " + myEntityViewModel.ListElement.Count);
        }
    }
}

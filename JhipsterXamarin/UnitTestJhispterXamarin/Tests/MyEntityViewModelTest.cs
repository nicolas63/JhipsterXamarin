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
        private Mock<IMyEntityService> myEntityService;
        private List<MyEntityModel> myEntities;
        private MyEntityViewModel myEntityViewModel;        
        private Fixture fixture;
        private MyEntityModel myEntityModel = new MyEntityModel();


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
            fixture = new Fixture();
            myEntityModel.Name = fixture.Create<string>();
            myEntityModel.Age = fixture.Create<int>();

            myEntities = new List<MyEntityModel>();
        }

        [TestMethod]
        public void Should_UpdateTheFirstEntity_When_NameOfFirstEntityChange()
        {
            //Arrange
            MyEntityModel sut = fixture.Create<MyEntityModel>();
            myEntityViewModel.ListElement.Add(sut);
            string expected = "the first entity updated";

            //Act
            myEntityViewModel.CurrentElement = myEntityViewModel.ListElement[0];
            myEntityViewModel.CurrentElement.Name = expected;
            string result = sut.Name;

            //Assert
            result.Should().Be(expected, "Test failed: We wanted : " + expected);

        }

        [TestMethod]
        public void Should_DeleteTheFirstEntity_When_DeleteButtonClicked()
        {
            //Arrange
            MyEntityModel sut = fixture.Create<MyEntityModel>();
            MyEntityModel sut2 = fixture.Create<MyEntityModel>();

            myEntityViewModel.ListElement.Add(sut);
            myEntityViewModel.ListElement.Add(sut2);


            //Act
            if (myEntityViewModel.ListElement.Contains(sut))
                myEntityViewModel.ListElement.Remove(sut);
                myEntityViewModel.CurrentElement = myEntityViewModel.ListElement[0];

            //Assert
            myEntityViewModel.ListElement.Count
                .Should().Be(1, "Test Failed : We wanted : " + 1);
            myEntityViewModel.CurrentElement
                .Should().Be(sut2, "Test Failed : we wanted : " + sut2.ToString());

        }
    }
}

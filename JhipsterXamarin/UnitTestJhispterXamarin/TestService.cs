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
        IMyEntityService myEntityService;
        MyEntityViewModel myEntityViewModel;
        MyEntityModelSimple myFirstEntityModelSimple = new MyEntityModel();

        [TestInitialize]
        public void TestViewModel()
        {
            initializeAll();
        }
        public void initializeAll()
        {
            base.Setup(); // from MvxIoCSupportingTest
            AdditionalSetup();
            InitializeMyEntityViewModel();
        }
        protected override void AdditionalSetup()
        {
            var mockAuthService = new Mock<IAuthenticationService>();
            var mockNavService = new Mock<IMvxNavigationService>();
            myEntityService = new MyEntityService(mockAuthService.Object._httpClient);
            myEntityViewModel = new MyEntityViewModel(mockNavService.Object, myEntityService);

            Ioc.RegisterSingleton<IAuthenticationService>(mockAuthService.Object);
            Ioc.RegisterSingleton<IMvxNavigationService>(mockNavService.Object);            
        }

        public void InitializeMyEntityViewModel()
        {
            myFirstEntityModelSimple.Name = "the first entity";
            myFirstEntityModelSimple.Age = 1;
            myEntityService.CreateEntity("the second entity", 2);
            myEntityService.CreateEntity("the third entity", 3);
            myEntityViewModel.Initialize();
        }

        [TestMethod]
        public void TestUpdate()
        {
            initializeAll();
            string expected = "the first entity updated";
            myFirstEntityModelSimple.Name = expected;
            myEntityService.UpdateEntity((MyEntityModel)myFirstEntityModelSimple);
            myEntityViewModel.CurrentElement.Name
                .Should().Be(expected, "Test failed: We wanted : " + expected + " We have :  " + myEntityViewModel.CurrentElement.Name);

        }
        [TestMethod]
        public void TestDelete()
        {
            initializeAll();
            myEntityService.DeleteEntity((MyEntityModel)myFirstEntityModelSimple);
            myEntityViewModel.ListElement.Count
                .Should().Be(2, "Test Failed : We wanted : " + 2 + " We have :  " + myEntityViewModel.ListElement.Count);
        }
    }
}

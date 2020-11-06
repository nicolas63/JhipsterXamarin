using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using Moq;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NUnit.Framework;

namespace UnitTestJhispterXamarin
{
    [TestFixture]
    public class TestService : MvxIoCSupportingTest
    {
        IMyEntityService myEntityService;
        MyEntityViewModel myEntityViewModel;
        MyEntityModelSimple myFirstEntityModelSimple = new MyEntityModel();

        [Test]
        public void TestViewModel()
        {
            initializeAll();
        }
        public void initializeAll()
        {
            base.ClearAll();
            base.Setup(); // from MvxIoCSupportingTest
            AdditionalSetup();
        }
        protected override void AdditionalSetup()
        {
            var mockAuthService = new Mock<IAuthenticationService>();
            var mockNavService = new Mock<IMvxNavigationService>();
            myEntityService = new MyEntityService(mockAuthService.Object._httpClient);
            Ioc.RegisterSingleton<IAuthenticationService>(mockAuthService.Object);
            myEntityViewModel = new MyEntityViewModel(mockNavService.Object, myEntityService);
            InitializeMyEntityViewModel();
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

        [Test]
        public void TestUpdate()
        {
            initializeAll();
            myFirstEntityModelSimple.Name = "the first entity updated";
            myEntityService.UpdateEntity((MyEntityModel)myFirstEntityModelSimple);
            var result = myEntityViewModel.CurrentElement;
            string expected = "the first entity updated";
            Assert.AreEqual(expected, result.Name, "Test failed: We wanted : " + expected + " We have :  " +result.Name);
        }
        [Test]
        public void TestDelete()
        {
            initializeAll();
            myEntityService.DeleteEntity((MyEntityModel)myFirstEntityModelSimple);
            Assert.AreEqual(2, myEntityViewModel.ListElement.Count, "Test Failed : We wanted : " + 2 + " We have :  " + myEntityViewModel.ListElement.Count);
        }
    }
}

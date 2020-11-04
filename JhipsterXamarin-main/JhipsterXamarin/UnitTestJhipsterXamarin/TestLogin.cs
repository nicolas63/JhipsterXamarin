using System;
using System.Net.Http;
using JhipsterXamarin.Models;
using JhipsterXamarin.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace TestUnitJhipsterXamarin
{
    [TestClass]
    public class TestLogin
    {
        /*
         * ###############################
         * ###  Login / Token / User  ####
         * ###############################
         */

        static IMvxNavigationService _navigationloginService;
        LoginViewModel login;

        [TestInitialize]
        public void InitializeLoginViewModel()
        {
            var mockLocator = new Mock<IMvxViewModelLocator>();
            mockLocator.Setup(
                            m => m.Load(It.IsAny<Type>(), It.IsAny<IMvxBundle>(), It.IsAny<IMvxBundle>(), It.IsAny<IMvxNavigateEventArgs>())).Returns(() => new LoginViewModel(_navigationloginService));

            var mockCollection = new Mock<IMvxViewModelLocatorCollection>();
            mockCollection.Setup(m => m.FindViewModelLocator(It.IsAny<MvxViewModelRequest>()))
                            .Returns(() => mockLocator.Object);

            var loader = new MvxViewModelLoader(mockCollection.Object);
            var cache = new MvxNavigationCache();

            IMvxNavigationService navigationloginService = new MvxNavigationService(cache, loader);

            Mvx.RegisterSingleton(mockLocator.Object);
            navigationloginService.LoadRoutes(new[] { typeof(LoginViewModel).Assembly });
            Mvx.RegisterSingleton<IMvxNavigationService>(navigationloginService);

            login = new LoginViewModel(navigationloginService);
            login.Initialize();
            Assert.Fail("Le test d'initialisation LoginViewModel a échoué");
        }


        LoginModel loginModel;

        [TestMethod]
        public void TestSignInAdmin()
        {
            //Echec :  InitializeLoginViewModel() failed
            InitializeLoginViewModel();

            loginModel.Username = "admin";
            loginModel.Password = "admin";
            loginModel.RememberMe = false;

            login.Username = "admin";
            login.Password = "admin";
            login.RememberMe = false;

            login.signIn();

            Assert.AreEqual(login._authenticationService.SignIn(loginModel), login._authenticationService.SignIn(login.Model), "Le test a échoué : erreur login user : admin  | password : admin");
        }

        [TestMethod]
        public void TestSignOutAdmin()
        {
            //Echec :  InitializeLoginViewModel() failed
            InitializeLoginViewModel();
            TestSignInAdmin();

            login._authenticationService.SignOut();

            Assert.AreEqual("No values", login._authenticationService.ToString(), "Le test a échoué : erreur logout");
        }
    }
}

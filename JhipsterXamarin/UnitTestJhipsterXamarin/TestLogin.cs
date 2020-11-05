using Moq;
using NUnit.Framework;
using MvvmCross.Tests;
using MvvmCross.ViewModels;
using UnitTestJhipsterXamarin;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using JhipsterXamarin.Models;
using JhipsterXamarin.ViewModels;
using System;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.Navigation;
using MvvmCross;

[TestFixture]
public class MyTest : MvxIoCSupportingTest
{
    LoginViewModel loginViewModel;
    LoginModel loginModel = new LoginModel();
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
        loginViewModel.Initialize();
    }
    protected override void AdditionalSetup()
    {
        var mockNavLoginService = new Mock<IMvxNavigationService>();
        loginViewModel = new LoginViewModel(mockNavLoginService.Object);

        Ioc.RegisterSingleton<IMvxNavigationService>(mockNavLoginService.Object);
        /*
        MockDispatcher = new MockDispatcher();
        Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
        Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
        */
    }
    protected MockDispatcher MockDispatcher
    {
        get;
        private set;
    }

    [Test]
    public void TestSignInAdmin()
    {
        initializeAll();
        //Echec :  InitializeLoginViewModel() failed

        loginModel.Username = "admin";
        loginModel.Password = "admin";
        loginModel.RememberMe = false;

        loginViewModel.Username = "admin";
        loginViewModel.Password = "admin";
        loginViewModel.RememberMe = false;

        loginViewModel.signIn();

        Assert.AreEqual(loginViewModel._authenticationService.SignIn(loginModel), loginViewModel._authenticationService.SignIn(loginViewModel.Model), "Le test a échoué : erreur login user : admin  | password : admin");
    }
    [Test]
    public void TestSignOutAdmin()
    {
        initializeAll();
        //Echec :  InitializeLoginViewModel() failed

        TestSignInAdmin();

        loginViewModel._authenticationService.SignOut();

        Assert.AreEqual("No values", loginViewModel._authenticationService.ToString(), "Le test a échoué : erreur logout");
    }
}

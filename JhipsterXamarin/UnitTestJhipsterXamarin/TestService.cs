using JhipsterXamarin.Services;
using JhipsterXamarin.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Navigation;

namespace TestUnitJhipsterXamarin
{
    [TestClass]
    public class TestService
    {
        /*
         * ##############################################
         * ###  ListViewModel / AjoutVal / SupprVal  ####
         * ##############################################
         */

        IListService listService = new ListService();
        ListViewModel listViewModel;
        readonly IMvxNavigationService _navigationService;
        [TestInitialize]
        public void InitializeListViewModel()
        {
            listService.GenerateList(0);
            listViewModel = new ListViewModel(_navigationService, listService);
            listViewModel.Initialize();
            Assert.AreEqual(5, listViewModel.NbElement, "Le test a échoué : Valeur attendues : " + 5 + " valeur réel :  " + listViewModel.NbElement);
        }

        [TestMethod]
        public void TestAjout()
        {
            InitializeListViewModel();
            listViewModel.NbElement++;
            Assert.AreEqual(6, listViewModel.NbElement, "Le test a échoué : Valeur attendues : " + 6 + " valeur réel :  " + listViewModel.NbElement);
        }
        [TestMethod]
        public void TestSupression()
        {
            InitializeListViewModel();
            listViewModel.NbElement--;
            Assert.AreEqual(4, listViewModel.NbElement, "Le test a échoué : Valeur attendues : " + 4 + " valeur réel :  " + listViewModel.NbElement);
        }
    }
}


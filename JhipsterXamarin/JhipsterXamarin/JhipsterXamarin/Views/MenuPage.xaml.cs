﻿using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using JhipsterXamarin.ViewModels;
using System;
using Xamarin.Forms;

namespace JhipsterXamarin.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MenuPage : MvxContentPage<MenuViewModel>
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        public void ToggleClicked(object sender, EventArgs e)
        {
            if (Parent is MvxMasterDetailPage md && Device.RuntimePlatform != Device.UWP)
            {
                md.MasterBehavior = MasterBehavior.Popover;
                md.IsPresented = !md.IsPresented;
            }
        }
    }
}
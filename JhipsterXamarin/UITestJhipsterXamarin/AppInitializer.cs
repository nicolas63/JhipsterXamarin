﻿using Android.OS;
using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITestJhipsterXamarin
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                .ApkFile("../JhipsterXamarin/JhipsterXamarin.Android/bin/Debug/com.companyname.jhipsterxamarin.apk")
                .DeviceSerial(deviceSerial: Build.Serial)
                .StartApp();
            }
            return ConfigureApp.iOS.StartApp();
        }
    }
}
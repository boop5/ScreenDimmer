using System;
using System.Windows;
using Autofac;
using SchadLucas.Configuration;
using SchadLucas.Wpf.EzMvvm.Context;
using SchadLucas.Wpf.EzMvvm.Core;
using ScreenDimmer.Configuration;
using ScreenDimmer.Root;
using ScreenDimmer.Screen;

namespace ScreenDimmer
{
    public sealed class ScreenDimmerBootstrapper : EzBootstrapper
    {
        private Window _rootWindow;
        private IViewModel _rootContext;

        public ScreenDimmerBootstrapper()
        {
            Initialize();
        }

        protected override IViewModel GetRootDataContext() => _rootContext ?? (_rootContext = Container.Resolve<RootContext>());
        protected override Window GetRootWindow() => _rootWindow ?? (_rootWindow = Container.Resolve<RootWindow>());

        protected override void RegisterModules(ContainerBuilder builder)
        {
            base.RegisterModules(builder);

            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var configPath = System.IO.Path.Combine(userPath, ".screendimmer");

            builder.RegisterInstance(new EzFileConfiguration(configPath)).As<EzConfiguration>().SingleInstance();
            builder.RegisterType<ConfigurationService>().SingleInstance();
            builder.RegisterType<ScreenService>().SingleInstance();
            builder.RegisterType<RootContext>().SingleInstance();
            builder.RegisterType<RootWindow>().SingleInstance();
        }
    }
}
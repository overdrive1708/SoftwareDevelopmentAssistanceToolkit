using Prism.Ioc;
using Prism.Modularity;
using SDAT.Modules.AboutInfo;
using SDAT.Modules.ConvertRadix;
using SDAT.Modules.WelcomeInfo;
using SDAT.Services;
using SDAT.Services.Interfaces;
using SDAT.Views;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SDAT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<IConvertRadixService, ConvertRadixService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<WelcomeInfoModule>();
            moduleCatalog.AddModule<AboutInfoModule>();
            moduleCatalog.AddModule<ConvertRadixModule>();
        }

        /// <summary>
        /// Startup処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            // 未処理の例外が発生したときの処理を登録する｡
            DispatcherUnhandledException += Core.ExceptionHandler.OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += Core.ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += Core.ExceptionHandler.OnUnhandledException;
        }
    }
}

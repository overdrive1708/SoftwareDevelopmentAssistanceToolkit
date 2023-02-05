using MainApplication.Modules.ModuleName;
using MainApplication.Services;
using MainApplication.Services.Interfaces;
using MainApplication.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MainApplication
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
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }

        /// <summary>
        /// Startup処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            // 未処理の例外を処理するイベントハンドラを登録する｡
            DispatcherUnhandledException += Core.ExceptionHandler.OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += Core.ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += Core.ExceptionHandler.OnUnhandledException;
        }
    }
}

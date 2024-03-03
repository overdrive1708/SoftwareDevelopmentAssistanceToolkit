using Prism.Ioc;
using Prism.Modularity;

namespace SDAT.Modules.CompareCLangDefine
{
    public class CompareCLangDefineModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 無処理
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.CompareCLangDefine>();
        }
    }
}
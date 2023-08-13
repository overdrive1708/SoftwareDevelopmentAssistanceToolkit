using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SDAT.Core;

namespace SDAT.Modules.WelcomeInfo
{
    public class WelcomeInfoModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public WelcomeInfoModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "WelcomeInfo");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.WelcomeInfo>();
        }
    }
}
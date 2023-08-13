using Prism.Regions;
using SDAT.Core.Mvvm;

namespace SDAT.Modules.WelcomeInfo.ViewModels
{
    public class WelcomeInfoViewModel : RegionViewModelBase
    {
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public WelcomeInfoViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">IRegionManager</param>
        public WelcomeInfoViewModel(IRegionManager regionManager) :
            base(regionManager)
        {
            // 無処理
        }
    }
}

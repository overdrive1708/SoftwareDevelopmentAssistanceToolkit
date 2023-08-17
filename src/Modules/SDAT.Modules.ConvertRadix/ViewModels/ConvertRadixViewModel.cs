using Prism.Regions;
using SDAT.Core.Mvvm;

namespace SDAT.Modules.ConvertRadix.ViewModels
{
    public class ConvertRadixViewModel : RegionViewModelBase
    {
        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public ConvertRadixViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">IRegionManager</param>
        public ConvertRadixViewModel(IRegionManager regionManager) :
            base(regionManager)
        {
            // 無処理
        }

        /// <summary>
        /// 画面遷移時処理
        /// </summary>
        /// <param name="navigationContext">ナビゲーションに関する情報</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 無処理
        }
    }
}

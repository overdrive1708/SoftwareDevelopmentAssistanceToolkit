using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SDAT.Core;

namespace SDAT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //--------------------------------------------------
        // バインディングデータ
        //--------------------------------------------------
        /// <summary>
        /// タイトル
        /// </summary>
        private string _title = Core.Resources.Strings.ApplicationName;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //--------------------------------------------------
        // バインディングコマンド(スニペット:cmd/cmdg)
        //--------------------------------------------------
        /// <summary>
        /// 画面遷移コマンド
        /// </summary>
        private DelegateCommand<string> _commandTransitionScreen;
        public DelegateCommand<string> CommandTransitionScreen =>
            _commandTransitionScreen ?? (_commandTransitionScreen = new DelegateCommand<string>(ExecuteCommandTransitionScreen));

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// 画面遷移管理情報
        /// </summary>
        private readonly IRegionManager _regionManager;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public MainWindowViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager"></param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            // 画面遷移管理情報を設定する
            _regionManager = regionManager;
        }

        /// <summary>
        /// 画面遷移コマンド実行処理
        /// </summary>
        /// <param name="screenName">画面遷移先のユーザーコントロール名</param>
        private void ExecuteCommandTransitionScreen(string screenName)
        {
            // 指定された画面に遷移する
            _regionManager.RequestNavigate(RegionNames.ContentRegion, screenName, navigationResult =>
            {
                if (navigationResult.Result == true)
                {
                    // 画面遷移成功時にタイトルを更新する
                    Title = screenName switch
                    {
                        "WelcomeInfo" => Core.Resources.Strings.ApplicationName,
                        "ConvertRadix" => $"{Core.Resources.Strings.ApplicationName} | {Resources.Strings.ScreenTitleConvertRadix}",
                        "CompareListItem" => $"{Core.Resources.Strings.ApplicationName} | {Resources.Strings.ScreenTitleCompareListItem}",
                        "CompareClangDefine" => $"{Core.Resources.Strings.ApplicationName} | {Resources.Strings.ScreenTitleCompareCLangDefine}",
                        "AboutInfo" => $"{Core.Resources.Strings.ApplicationName} | {Resources.Strings.ScreenTitleAboutInfo}",
                        _ => Core.Resources.Strings.ApplicationName,
                    };
                }
            });
        }
    }
}

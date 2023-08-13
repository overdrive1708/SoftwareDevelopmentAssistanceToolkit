using Prism.Commands;
using Prism.Regions;
using SDAT.Core.Mvvm;
using System.Diagnostics;
using System.Reflection;

namespace SDAT.Modules.AboutInfo.ViewModels
{
    public class AboutInfoViewModel : RegionViewModelBase
    {
        //--------------------------------------------------
        // バインディングデータ(スニペット:propp)
        //--------------------------------------------------
        /// <summary>
        /// 製品名
        /// </summary>
        private string _productBody = Core.Resources.Strings.ApplicationName;
        public string ProductBody
        {
            get { return _productBody; }
            set { SetProperty(ref _productBody, value); }
        }

        /// <summary>
        /// ライセンス
        /// </summary>
        private string _licenseBody = string.Empty;
        public string LicenseBody
        {
            get { return _licenseBody; }
            set { SetProperty(ref _licenseBody, value); }
        }

        //--------------------------------------------------
        // バインディングコマンド(スニペット:cmd/cmdg)
        //--------------------------------------------------
        /// <summary>
        /// URLを開く
        /// </summary>
        private DelegateCommand<string> _commandOpenUrl;
        public DelegateCommand<string> CommandOpenUrl =>
            _commandOpenUrl ?? (_commandOpenUrl = new DelegateCommand<string>(ExecuteCommandOpenUrl));

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public AboutInfoViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">IRegionManager</param>
        public AboutInfoViewModel(IRegionManager regionManager) :
            base(regionManager)
        {
            // エントリポイントのアセンブリ情報を取得して表示値を更新
            Assembly assm = Assembly.GetEntryAssembly();
            if (assm != null)
            {
                LicenseBody = $"{assm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright}\r\n{Core.Resources.Strings.MessageAboutInfoLibrariesBody}";
            }
        }

        /// <summary>
        /// URLを開くコマンド実行処理
        /// </summary>
        private void ExecuteCommandOpenUrl(string url)
        {
            ProcessStartInfo psi = new()
            {
                FileName = url,
                UseShellExecute = true,
            };
            Process.Start(psi);
        }
    }
}

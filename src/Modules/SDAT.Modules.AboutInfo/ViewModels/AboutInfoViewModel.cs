﻿using Prism.Commands;
using Prism.Navigation.Regions;
using SDAT.Core.Mvvm;
using SDAT.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace SDAT.Modules.AboutInfo.ViewModels
{
    /// <summary>
    /// バージョン情報クラス
    /// </summary>
    public class VersionInfo
    {
        public string Component { get; set; }   // コンポーネント
        public string Version { get; set; }     // バージョン
    }

    public class AboutInfoViewModel : RegionViewModelBase
    {
        //--------------------------------------------------
        // バインディングデータ(スニペット:propp)
        //--------------------------------------------------
        /// <summary>
        /// 製品名
        /// </summary>
        private string _productBody = string.Empty;
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

        /// <summary>
        /// バージョン情報
        /// </summary>
        private ObservableCollection<VersionInfo> _versionInfoData = new();
        public ObservableCollection<VersionInfo> VersionInfoData
        {
            get { return _versionInfoData; }
            set { SetProperty(ref _versionInfoData, value); }
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

        /// <summary>
        /// バージョン情報出力
        /// </summary>
        private DelegateCommand _commandOutputVersionInfo;
        public DelegateCommand CommandOutputVersionInfo =>
            _commandOutputVersionInfo ?? (_commandOutputVersionInfo = new DelegateCommand(ExecuteCommandOutputVersionInfo));

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// MessageService
        /// </summary>
        private readonly IMessageService _messageService;

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
        public AboutInfoViewModel(IRegionManager regionManager, IMessageService messageService) :
            base(regionManager)
        {
            // MessageServiceの情報を設定する
            _messageService = messageService;
        }

        /// <summary>
        /// 画面遷移時処理
        /// </summary>
        /// <param name="navigationContext">ナビゲーションに関する情報</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            VersionInfo versionInfo;

            VersionInfoData.Clear();

            // 製品名情報表示値の更新
            ProductBody = _messageService.GetProductInfo();

            // ライセンス情報表示値の更新
            LicenseBody = $"{_messageService.GetCopyrightInfo()}\r\n{Resources.Strings.MessageAboutInfoFollowingURL}";

            // バージョン情報表示値の更新(メインアプリケーション)
            versionInfo = new()
            {
                Component = "SDAT.dll",
                Version = _messageService.GetVersionInfoMain()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Core)
            versionInfo = new()
            {
                Component = "SDAT.Core.dll",
                Version = _messageService.GetVersionInfoCore()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Modules.ConvertRadix)
            versionInfo = new()
            {
                Component = "SDAT.Modules.ConvertRadix.dll",
                Version = _messageService.GetVersionInfoModulesConvertRadix()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Modules.CompareListItem)
            versionInfo = new()
            {
                Component = "SDAT.Modules.CompareListItem.dll",
                Version = _messageService.GetVersionInfoModulesCompareListItem()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Modules.CompareCLangDefine)
            versionInfo = new()
            {
                Component = "SDAT.Modules.CompareCLangDefine.dll",
                Version = _messageService.GetVersionInfoModulesCompareCLangDefine()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Modules.WelcomeInfo)
            versionInfo = new()
            {
                Component = "SDAT.Modules.WelcomeInfo.dll",
                Version = _messageService.GetVersionInfoModulesWelcomeInfo()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Modules.AboutInfo)
            versionInfo = new()
            {
                Component = "SDAT.Modules.AboutInfo.dll",
                Version = _messageService.GetVersionInfoModulesAboutInfo()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Services)
            versionInfo = new()
            {
                Component = "SDAT.Services.dll",
                Version = _messageService.GetVersionInfoServices()
            };
            VersionInfoData.Add(versionInfo);

            // バージョン情報表示値の更新(Services.Interfaces)
            versionInfo = new()
            {
                Component = "SDAT.Services.Interfaces.dll",
                Version = _messageService.GetVersionInfoServicesInterfaces()
            };
            VersionInfoData.Add(versionInfo);
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

        /// <summary>
        /// バージョン情報出力コマンド実行処理
        /// </summary>
        private void ExecuteCommandOutputVersionInfo()
        {
            string copytext = $"{Resources.Strings.Component},{Resources.Strings.Version}";

            foreach (VersionInfo versionInfo in VersionInfoData)
            {
                copytext = $"{copytext}\r\n{versionInfo.Component},{versionInfo.Version}";
            }

            Clipboard.SetText(copytext);

            MessageBox.Show(Resources.Strings.MessageCompleteOutputVersionInfo, Resources.Strings.Complete, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

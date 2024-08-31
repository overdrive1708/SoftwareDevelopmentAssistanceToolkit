using Prism.Commands;
using Prism.Navigation.Regions;
using SDAT.Core.Mvvm;
using SDAT.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SDAT.Modules.CompareCLangDefine.ViewModels
{
    public class CompareCLangDefineViewModel : RegionViewModelBase
    {
        //--------------------------------------------------
        // バインディングデータ(スニペット:propp)
        //--------------------------------------------------
        /// <summary>
        /// 入力有効フラグ
        /// </summary>
        private bool _isEnableInput = true;
        public bool IsEnableInput
        {
            get { return _isEnableInput; }
            set { SetProperty(ref _isEnableInput, value); }
        }

        /// <summary>
        /// 変更前定義
        /// </summary>
        private string _beforeDefines = string.Empty;
        public string BeforeDefines
        {
            get { return _beforeDefines; }
            set { SetProperty(ref _beforeDefines, value); }
        }

        /// <summary>
        /// 変更後定義
        /// </summary>
        private string _afterDefines = string.Empty;
        public string AfterDefines
        {
            get { return _afterDefines; }
            set { SetProperty(ref _afterDefines, value); }
        }

        /// <summary>
        /// 追加定義
        /// </summary>
        private string _addDefines = string.Empty;
        public string AddDefines
        {
            get { return _addDefines; }
            set { SetProperty(ref _addDefines, value); }
        }

        /// <summary>
        /// 削除定義
        /// </summary>
        private string _deleteDefines = string.Empty;
        public string DeleteDefines
        {
            get { return _deleteDefines; }
            set { SetProperty(ref _deleteDefines, value); }
        }

        /// <summary>
        /// 変更定義
        /// </summary>
        private ObservableCollection<ChangeCLangDefineInfo> _changeDefines = new();
        public ObservableCollection<ChangeCLangDefineInfo> ChangeDefines
        {
            get { return _changeDefines; }
            set { SetProperty(ref _changeDefines, value); }
        }

        /// <summary>
        /// 比較実行ボタンテキスト
        /// </summary>
        private string _compareState = Resources.Strings.Compare;
        public string CompareState
        {
            get { return _compareState; }
            set { SetProperty(ref _compareState, value); }
        }

        /// <summary>
        /// 比較実行ボタン有効フラグ
        /// </summary>
        private bool _isEnableCompare = true;
        public bool IsEnableCompare
        {
            get { return _isEnableCompare; }
            set { SetProperty(ref _isEnableCompare, value); }
        }

        /// <summary>
        /// 比較実行ボタンプログレスバー表示フラグ
        /// </summary>
        private bool _isProgressIndicatorVisible = false;
        public bool IsProgressIndicatorVisible
        {
            get { return _isProgressIndicatorVisible; }
            set { SetProperty(ref _isProgressIndicatorVisible, value); }
        }

        /// <summary>
        /// 比較実行ボタンプログレスバー不定フラグ
        /// </summary>
        private bool _isProgressIndeterminate;
        public bool IsProgressIndeterminate
        {
            get { return _isProgressIndeterminate; }
            set { SetProperty(ref _isProgressIndeterminate, value); }
        }

        /// <summary>
        /// 現在進捗率
        /// </summary>
        private int _nowProgress = 0;
        public int NowProgress
        {
            get { return _nowProgress; }
            set { SetProperty(ref _nowProgress, value); }
        }

        //--------------------------------------------------
        // バインディングコマンド(スニペット:cmd/cmdg)
        //--------------------------------------------------
        /// <summary>
        /// 比較実行コマンド
        /// </summary>
        private DelegateCommand _commandCompare;
        public DelegateCommand CommandCompare =>
            _commandCompare ?? (_commandCompare = new DelegateCommand(ExecuteCommandCompare));

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// 比較完了後操作有効待ちタイマ
        /// </summary>
        private readonly DispatcherTimer _timerCompareEnableWait;

        /// <summary>
        /// CompareService
        /// </summary>
        private readonly ICompareCLangDefineService _compareService;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public CompareCLangDefineViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">IRegionManager</param>
        /// <param name="compareService">ICompareService</param>
        public CompareCLangDefineViewModel(IRegionManager regionManager, ICompareCLangDefineService compareService) :
            base(regionManager)
        {
            // CompareServiceの情報を設定する
            _compareService = compareService;

            // タイマ設定
            _timerCompareEnableWait = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            _timerCompareEnableWait.Tick += new EventHandler(TimeoutCompareEnableWait);
        }

        /// <summary>
        /// 比較実行コマンド実行処理
        /// </summary>
        private async void ExecuteCommandCompare()
        {
            CompareCLangDefineResult compareResult;
            Progress<int> progress = new(OnProgressChanged);

            // 比較開始
            IsEnableCompare = false;
            IsEnableInput = false;
            IsProgressIndeterminate = true;
            IsProgressIndicatorVisible = true;
            CompareState = Resources.Strings.MessageProcessing;

            // 比較実施
            await Task.Run(() =>
            {
                compareResult = _compareService.GetCLangDefineCompareResult(BeforeDefines, AfterDefines, progress);
                AddDefines = compareResult.AddDefines;
                DeleteDefines = compareResult.DeleteDefines;
                ChangeDefines = new ObservableCollection<ChangeCLangDefineInfo>(compareResult.ChangeDefines);
            });

            // 比較完了
            CompareState = Resources.Strings.MessageComplete;
            IsEnableInput = true;
            _timerCompareEnableWait.Start();
        }

        /// <summary>
        /// 比較完了後操作有効待ちタイマのタイムアウト処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void TimeoutCompareEnableWait(object sender, EventArgs e)
        {
            _timerCompareEnableWait.Stop();
            CompareState = Resources.Strings.Compare;
            IsEnableCompare = true;
            IsProgressIndicatorVisible = false;
            IsProgressIndeterminate = false;
        }

        /// <summary>
        /// 進捗更新処理
        /// </summary>
        /// <param name="percentage">進捗率</param>
        private void OnProgressChanged(int percentage)
        {
            if (percentage == ICompareListItemService.ProgressUnknown)
            {
                IsProgressIndeterminate = true;
                CompareState = Resources.Strings.MessageProcessing;
            }
            else
            {
                IsProgressIndeterminate = false;
                NowProgress = percentage;
                CompareState = $"{Resources.Strings.MessageProcessing}({percentage}%)";
            }
        }
    }
}

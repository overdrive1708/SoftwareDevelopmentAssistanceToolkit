using Prism.Commands;
using Prism.Regions;
using SDAT.Core.Mvvm;
using SDAT.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SDAT.Modules.CompareListItem.ViewModels
{
    public class CompareListItemViewModel : RegionViewModelBase
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
        /// 変更前リスト項目
        /// </summary>
        private string _beforeListItems = string.Empty;
        public string BeforeListItems
        {
            get { return _beforeListItems; }
            set { SetProperty(ref _beforeListItems, value); }
        }

        /// <summary>
        /// 変更後リスト項目
        /// </summary>
        private string _afterListItems = string.Empty;
        public string AfterListItems
        {
            get { return _afterListItems; }
            set { SetProperty(ref _afterListItems, value); }
        }

        /// <summary>
        /// 追加リスト項目
        /// </summary>
        private string _addListItems = string.Empty;
        public string AddListItems
        {
            get { return _addListItems; }
            set { SetProperty(ref _addListItems, value); }
        }

        /// <summary>
        /// 削除リスト項目
        /// </summary>
        private string _deleteListItems = string.Empty;
        public string DeleteListItems
        {
            get { return _deleteListItems; }
            set { SetProperty(ref _deleteListItems, value); }
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
        private readonly ICompareListItemService _compareService;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public CompareListItemViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">IRegionManager</param>
        /// <param name="compareService">ICompareService</param>
        public CompareListItemViewModel(IRegionManager regionManager, ICompareListItemService compareService) :
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
            CompareResult compareResult;
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
                compareResult = _compareService.GetListItemCompareResult(BeforeListItems, AfterListItems, progress);
                AddListItems = compareResult.AddListItems;
                DeleteListItems = compareResult.DeleteListItems;
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

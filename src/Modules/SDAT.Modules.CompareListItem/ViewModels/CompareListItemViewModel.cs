using Prism.Commands;
using Prism.Mvvm;
using SDAT.Core;

namespace SDAT.Modules.CompareListItem.ViewModels
{
    public class CompareListItemViewModel : BindableBase
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
        /// 進捗状況データ
        /// </summary>
        private ProgressInfo _progressInfoData = new();
        public ProgressInfo ProgressInfoData
        {
            get { return _progressInfoData; }
            set { SetProperty(ref _progressInfoData, value); }
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
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CompareListItemViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// 比較実行コマンド実行処理
        /// </summary>
        private void ExecuteCommandCompare()
        {
            // TODO:処理実装
        }
    }
}

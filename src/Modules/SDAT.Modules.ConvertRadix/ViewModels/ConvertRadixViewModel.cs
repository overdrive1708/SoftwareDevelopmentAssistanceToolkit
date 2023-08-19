using Prism.Commands;
using Prism.Regions;
using SDAT.Core.Mvvm;
using SDAT.Services.Interfaces;

namespace SDAT.Modules.ConvertRadix.ViewModels
{
    public class ConvertRadixViewModel : RegionViewModelBase
    {
        //--------------------------------------------------
        // バインディングデータ(スニペット:propp)
        //--------------------------------------------------
        /// <summary>
        /// 2進数からの変換：2進数
        /// </summary>
        private string _stringBin;
        public string StringBin
        {
            get { return _stringBin; }
            set { SetProperty(ref _stringBin, value); }
        }

        /// <summary>
        /// 2進数からの変換：10進数
        /// </summary>
        private string _stringBinToDec;
        public string StringBinToDec
        {
            get { return _stringBinToDec; }
            set { SetProperty(ref _stringBinToDec, value); }
        }

        /// <summary>
        /// 2進数からの変換：16進数
        /// </summary>
        private string _stringBinToHex;
        public string StringBinToHex
        {
            get { return _stringBinToHex; }
            set { SetProperty(ref _stringBinToHex, value); }
        }

        /// <summary>
        /// 10進数からの変換：10進数
        /// </summary>
        private string _stringDec;
        public string StringDec
        {
            get { return _stringDec; }
            set { SetProperty(ref _stringDec, value); }
        }

        /// <summary>
        /// 10進数からの変換：2進数
        /// </summary>
        private string _stringDecToBin;
        public string StringDecToBin
        {
            get { return _stringDecToBin; }
            set { SetProperty(ref _stringDecToBin, value); }
        }

        /// <summary>
        /// 10進数からの変換：16進数
        /// </summary>
        private string _stringDecToHex;
        public string StringDecToHex
        {
            get { return _stringDecToHex; }
            set { SetProperty(ref _stringDecToHex, value); }
        }

        /// <summary>
        /// 16進数からの変換：16進数
        /// </summary>
        private string _stringHex;
        public string StringHex
        {
            get { return _stringHex; }
            set { SetProperty(ref _stringHex, value); }
        }

        /// <summary>
        /// 16進数からの変換：2進数
        /// </summary>
        private string _stringHexToBin;
        public string StringHexToBin
        {
            get { return _stringHexToBin; }
            set { SetProperty(ref _stringHexToBin, value); }
        }

        /// <summary>
        /// 16進数からの変換：10進数
        /// </summary>
        private string _stringHexToDec;
        public string StringHexToDec
        {
            get { return _stringHexToDec; }
            set { SetProperty(ref _stringHexToDec, value); }
        }

        //--------------------------------------------------
        // バインディングコマンド(スニペット:cmd/cmdg)
        //--------------------------------------------------
        /// <summary>
        /// 基数変換(2進数から)
        /// </summary>
        private DelegateCommand _commandConvertFromBin;
        public DelegateCommand CommandConvertFromBin =>
            _commandConvertFromBin ?? (_commandConvertFromBin = new DelegateCommand(ExecuteCommandConvertFromBin));

        /// <summary>
        /// 基数変換(10進数から)
        /// </summary>
        private DelegateCommand _commandConvertFromDec;
        public DelegateCommand CommandConvertFromDec =>
            _commandConvertFromDec ?? (_commandConvertFromDec = new DelegateCommand(ExecuteCommandConvertFromDec));

        /// <summary>
        /// 基数変換(16進数から)
        /// </summary>
        private DelegateCommand _commandConvertFromHex;
        public DelegateCommand CommandConvertFromHex =>
            _commandConvertFromHex ?? (_commandConvertFromHex = new DelegateCommand(ExecuteCommandConvertFromHex));

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// ConvertRadixService
        /// </summary>
        private readonly IConvertRadixService _convertRadixService;

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
        /// <param name="convertRadixService">IConvertRadixService</param>
        public ConvertRadixViewModel(IRegionManager regionManager, IConvertRadixService convertRadixService) :
            base(regionManager)
        {
            // ConvertRadixServiceの情報を設定する
            _convertRadixService = convertRadixService;
        }

        /// <summary>
        /// 画面遷移時処理
        /// </summary>
        /// <param name="navigationContext">ナビゲーションに関する情報</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // バインディングデータを初期化する
            StringBin = string.Empty;
            StringBinToDec = string.Empty;
            StringBinToHex = string.Empty;
            StringDec = string.Empty;
            StringDecToBin = string.Empty;
            StringDecToHex = string.Empty;
            StringHex = string.Empty;
            StringHexToBin = string.Empty;
            StringHexToDec = string.Empty;
        }

        /// <summary>
        /// 基数変換(2進数から)コマンド実行処理
        /// </summary>
        private void ExecuteCommandConvertFromBin()
        {
            StringBinToDec = _convertRadixService.ConvertRadixBinToDec(StringBin);
            StringBinToHex = _convertRadixService.ConvertRadixBinToHex(StringBin);
        }

        /// <summary>
        /// 基数変換(10進数から)コマンド実行処理
        /// </summary>
        private void ExecuteCommandConvertFromDec()
        {
            StringDecToBin = _convertRadixService.ConvertRadixDecToBin(StringDec);
            StringDecToHex = _convertRadixService.ConvertRadixDecToHex(StringDec);
        }

        /// <summary>
        /// 基数変換(16進数から)コマンド実行処理
        /// </summary>
        private void ExecuteCommandConvertFromHex()
        {
            StringHexToBin = _convertRadixService.ConvertRadixHexToBin(StringHex);
            StringHexToDec = _convertRadixService.ConvertRadixHexToDec(StringHex);
        }
    }
}

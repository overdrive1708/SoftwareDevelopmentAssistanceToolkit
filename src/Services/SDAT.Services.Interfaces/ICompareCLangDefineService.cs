using System;
using System.Collections.Generic;

namespace SDAT.Services.Interfaces
{
    /// <summary>
    /// 変更定義情報
    /// </summary>
    public class ChangeCLangDefineInfo
    {
        /// <summary>
        /// 定義名
        /// </summary>
        public string Define { get; set; } = string.Empty;

        /// <summary>
        /// 変更前定義値
        /// </summary>
        public string BeforeValue { get; set; } = string.Empty;

        /// <summary>
        /// 変更後定義値
        /// </summary>
        public string AfterValue { get; set; } = string.Empty;
    }

    /// <summary>
    /// 比較結果
    /// </summary>
    public class CompareCLangDefineResult
    {
        /// <summary>
        /// 追加定義
        /// </summary>
        public string AddDefines { get; set; } = string.Empty;

        /// <summary>
        /// 削除定義
        /// </summary>
        public string DeleteDefines { get; set; } = string.Empty;

        /// <summary>
        /// 変更定義
        /// </summary>
        public List<ChangeCLangDefineInfo> ChangeDefines { get; set; } = new List<ChangeCLangDefineInfo>();
    }

    public interface ICompareCLangDefineService
    {
        /// <summary>
        ///  進捗率：不明
        /// </summary>
        public static readonly int ProgressUnknown = 255;

        /// <summary>
        /// C言語定義比較結果取得処理
        /// </summary>
        /// <param name="beforeDefines">変更前定義</param>
        /// <param name="afterDefines">変更後定義</param>
        /// <param name="progress">進捗率報告用情報</param>
        /// <returns>比較結果</returns>
        CompareCLangDefineResult GetCLangDefineCompareResult(string beforeDefines, string afterDefines, IProgress<int> progress);
    }
}

using System;

namespace SDAT.Services.Interfaces
{
    /// <summary>
    /// 比較結果
    /// </summary>
    public class CompareResult
    {
        /// <summary>
        /// 追加リスト項目
        /// </summary>
        public string AddListItems { get; set; } = string.Empty;

        /// <summary>
        /// 削除リスト項目
        /// </summary>
        public string DeleteListItems { get; set; } = string.Empty;
    }

    public interface ICompareListItemService
    {
        /// <summary>
        ///  進捗率：不明
        /// </summary>
        public static readonly int ProgressUnknown = 255;

        /// <summary>
        /// リスト項目比較結果取得処理
        /// </summary>
        /// <param name="beforeListItems">変更前リスト項目</param>
        /// <param name="afterListItems">変更後リスト項目</param>
        /// <param name="progress">進捗率報告用情報</param>
        /// <returns>比較結果</returns>
        CompareResult GetListItemCompareResult(string beforeListItems, string afterListItems, IProgress<int> progress);
    }
}

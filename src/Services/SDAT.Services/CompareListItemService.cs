using SDAT.Services.Interfaces;
using System;

namespace SDAT.Services
{
    public class CompareListItemService : ICompareListItemService
    {
        /// <summary>
        /// リスト項目比較結果取得処理
        /// </summary>
        /// <param name="beforeListItems">変更前リスト項目</param>
        /// <param name="afterListItems">変更後リスト項目</param>
        /// <param name="progress">進捗率報告用情報</param>
        /// <returns>比較結果</returns>
        public CompareResult GetListItemCompareResult(string beforeListItems, string afterListItems, IProgress<int> progress)
        {
            CompareResult compareResult = new CompareResult();

            // 進捗率が計算できる状態になるまで｢不明｣を通知する
            progress.Report(ICompareListItemService.ProgressUnknown);

            //TODO:比較

            progress.Report(100);

            return compareResult;
        }
    }
}

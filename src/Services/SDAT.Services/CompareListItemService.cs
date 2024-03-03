using SDAT.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDAT.Services
{
    public class CompareListItemService : ICompareListItemService
    {
        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// 変更前アイテムリスト
        /// </summary>
        private List<string> _beforeItemList;

        /// <summary>
        /// 変更後アイテムリスト
        /// </summary>
        private List<string> _afterItemList;

        /// <summary>
        /// 進捗率計算用現在処理数
        /// </summary>
        private int _progressNow;

        /// <summary>
        /// 進捗率計算用総処理数
        /// </summary>
        private int _progressAll;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
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
            _beforeItemList = new List<string>();
            _afterItemList = new List<string>();

            // 進捗率が計算できる状態になるまで｢不明｣を通知する
            progress.Report(ICompareListItemService.ProgressUnknown);

            // 変更前アイテムリスト生成
            CreateBeforeItemList(beforeListItems);

            // 変更後アイテムリスト生成
            CreateAfterItemList(afterListItems);

            // 変更前後がないなら処理終了
            if ((_beforeItemList.Count == 0) && (_afterItemList.Count == 0))
            {
                return compareResult;
            }

            progress.Report(0);

            // 追加リスト項目生成
            compareResult.AddListItems = GetAddItem();
            progress.Report(50);

            // 削除リスト項目生成
            compareResult.DeleteListItems = GetDelItem();
            progress.Report(100);

            return compareResult;
        }

        /// <summary>
        /// 変更前アイテムリスト生成処理
        /// </summary>
        /// <param name="beforeListItems">変更前リスト項目</param>
        private void CreateBeforeItemList(string beforeListItems)
        {
            _beforeItemList.Clear();

            string[] items = beforeListItems.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string item in items)
            {
                if (!_beforeItemList.Any(value => value == item))
                {
                    _beforeItemList.Add(item);
                }
            }
        }

        /// <summary>
        /// 変更後アイテムリスト生成処理
        /// </summary>
        /// <param name="afterListItems">変更後リスト項目</param>
        private void CreateAfterItemList(string afterListItems)
        {
            _afterItemList.Clear();

            string[] items = afterListItems.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string item in items)
            {
                if (!_afterItemList.Any(value => value == item))
                {
                    _afterItemList.Add(item);
                }
            }
        }

        /// <summary>
        /// 追加リスト項目生成処理
        /// </summary>
        /// <returns>比較結果</returns>
        private string GetAddItem()
        {
            string addItem = string.Empty;

            foreach (string item in _afterItemList)
            {
                if (!_beforeItemList.Any(value => value == item))
                {
                    addItem = $"{addItem}{item}\r\n";
                }
            }

            return addItem;
        }

        /// <summary>
        /// 削除リスト項目生成処理
        /// </summary>
        /// <returns>比較結果</returns>
        private string GetDelItem()
        {
            string delItem = string.Empty;

            foreach (string item in _beforeItemList)
            {
                if (!_afterItemList.Any(value => value == item))
                {
                    delItem = $"{delItem}{item}\r\n";
                }
            }

            return delItem;
        }
    }
}

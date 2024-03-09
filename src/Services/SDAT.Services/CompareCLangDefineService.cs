using SDAT.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SDAT.Services
{
    public class CompareCLangDefineService : ICompareCLangDefineService
    {
        //--------------------------------------------------
        // 内部定義
        //--------------------------------------------------
        /// <summary>
        /// 定義情報
        /// </summary>
        private class DefineInfo
        {
            /// <summary>
            /// 定義名
            /// </summary>
            public string Define { get; set; } = string.Empty;

            /// <summary>
            /// 定義値
            /// </summary>
            public string Value { get; set; } = string.Empty;
        }

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// 変更前定義リスト
        /// </summary>
        private List<DefineInfo> _beforeDefineList;

        /// <summary>
        /// 変更後定義リスト
        /// </summary>
        private List<DefineInfo> _afterDefineList;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// C言語定義比較結果取得処理
        /// </summary>
        /// <param name="beforeDefines">変更前定義</param>
        /// <param name="afterDefines">変更後定義</param>
        /// <param name="progress">進捗率報告用情報</param>
        /// <returns>比較結果</returns>
        public CompareCLangDefineResult GetCLangDefineCompareResult(string beforeDefines, string afterDefines, IProgress<int> progress)
        {
            CompareCLangDefineResult compareResult = new CompareCLangDefineResult();
            _beforeDefineList = new List<DefineInfo>();
            _afterDefineList = new List<DefineInfo>();

            // 進捗率が計算できる状態になるまで｢不明｣を通知する
            progress.Report(ICompareListItemService.ProgressUnknown);

            // 変更前定義リスト生成
            CreateBeforeDefineList(beforeDefines);

            // 変更後定義リスト生成
            CreateAfterDefineList(afterDefines);

            // 変更前後がないなら処理終了
            if ((_beforeDefineList.Count == 0) && (_afterDefineList.Count == 0))
            {
                return compareResult;
            }

            progress.Report(0);

            // 追加定義生成
            compareResult.AddDefines = GetAddDefine();
            progress.Report(30);

            // 削除定義生成
            compareResult.DeleteDefines = GetDelDefine();
            progress.Report(60);

            // 変更定義生成
            compareResult.ChangeDefines = GetChangeDefine();
            progress.Report(100);

            return compareResult;
        }

        /// <summary>
        /// 変更前定義リスト生成処理
        /// </summary>
        /// <param name="beforeDefines">変更前定義</param>
        private void CreateBeforeDefineList(string beforeDefines)
        {
            _beforeDefineList.Clear();

            string[] items = beforeDefines.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string item in items)
            {
                DefineInfo addlist;

                Match match = Regex.Match(item, @"#define[\t ]*(?<definename>[^\t ]*)[\t ]*(?<definevalue>.*)");
                if (match.Success)
                {
                    addlist = new DefineInfo
                    {
                        Define = match.Result("${definename}"),
                        Value = match.Result("${definevalue}")
                    };

                    if (!_beforeDefineList.Any(item => item.Define == addlist.Define && item.Value == addlist.Value))
                    {
                        _beforeDefineList.Add(addlist);
                    }
                }
            }
        }

        /// <summary>
        /// 変更後定義リスト生成処理
        /// </summary>
        /// <param name="afterDefines">変更後定義</param>
        private void CreateAfterDefineList(string afterDefines)
        {
            _afterDefineList.Clear();

            string[] items = afterDefines.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string item in items)
            {
                Match match = Regex.Match(item, @"#define[\t ]*(?<definename>[^\t ]*)[\t ]*(?<definevalue>.*)");
                if (match.Success)
                {
                    DefineInfo addlist = new DefineInfo
                    {
                        Define = match.Result("${definename}"),
                        Value = match.Result("${definevalue}")
                    };

                    if (!_afterDefineList.Any(item => item.Define == addlist.Define && item.Value == addlist.Value))
                    {
                        _afterDefineList.Add(addlist);
                    }
                }
            }
        }

        /// <summary>
        /// 追加定義生成処理
        /// </summary>
        /// <returns>比較結果</returns>
        private string GetAddDefine()
        {
            string addDefine = string.Empty;

            foreach(DefineInfo defineinfo in _afterDefineList)
            {
                if (!_beforeDefineList.Any(item => item.Define == defineinfo.Define))
                {
                    addDefine = $"{addDefine}{defineinfo.Define}\r\n";
                }
            }

            return addDefine;
        }

        /// <summary>
        /// 削除定義生成処理
        /// </summary>
        /// <returns>比較結果</returns>
        private string GetDelDefine()
        {
            string delDefine = string.Empty;

            foreach (DefineInfo defineinfo in _beforeDefineList)
            {
                if (!_afterDefineList.Any(item => item.Define == defineinfo.Define))
                {
                    delDefine = $"{delDefine}{defineinfo.Define}\r\n";
                }
            }

            return delDefine;
        }

        /// <summary>
        /// 変更定義生成処理
        /// </summary>
        /// <returns>比較結果</returns>
        private List<ChangeCLangDefineInfo> GetChangeDefine()
        {
            List<ChangeCLangDefineInfo> changeDefine = new List<ChangeCLangDefineInfo>();

            foreach (DefineInfo afterDefineInfo in _afterDefineList)
            {
                DefineInfo beforeDefineInfo = _beforeDefineList.FirstOrDefault(item => item.Define == afterDefineInfo.Define && item.Value != afterDefineInfo.Value);
                if (beforeDefineInfo != null)
                {
                    ChangeCLangDefineInfo changeinfo = new ChangeCLangDefineInfo()
                    {
                        Define = afterDefineInfo.Define,
                        BeforeValue = beforeDefineInfo.Value,
                        AfterValue = afterDefineInfo.Value
                    };
                    changeDefine.Add(changeinfo);
                }
            }

            return changeDefine;
        }
    }
}

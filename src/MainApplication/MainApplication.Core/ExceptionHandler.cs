﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MainApplication.Core
{
    public class ExceptionHandler
    {
        //--------------------------------------------------
        // 定数
        //--------------------------------------------------
        /// <summary>
        /// 例外情報記録ファイル
        /// </summary>
        private const string FatalErrorInformationPath = @"FatalErrorInformation.log";
        /// <summary>
        /// メモ帳の実行ファイル
        /// </summary>
        private const string NotepadPath = "notepad.exe";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// DispatcherUnhandledExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) => HandleException(e.Exception);

        /// <summary>
        /// UnobservedTaskExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) => HandleException(e.Exception.InnerException);

        /// <summary>
        /// UnhandledExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException((Exception)e.ExceptionObject);

        /// <summary>
        /// 例外発生時の処理
        /// </summary>
        /// <param name="e">例外情報</param>
        private static void HandleException(Exception exception)
        {
            // 例外の詳細情報を表示するか確認する｡
            MessageBoxResult result = MessageBox.Show(Resources.Text.MessageFatalError,
                                                      Resources.Text.ImportantNotice,
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Error);

            // 例外の詳細情報を表示する場合､ファイルに出力してメモ帳で開く｡
            if (result == MessageBoxResult.Yes)
            {
                using (StreamWriter fatalErrorInformationFile = new(FatalErrorInformationPath, false, System.Text.Encoding.UTF8))
                {
                    fatalErrorInformationFile.Write(exception.ToString());
                    fatalErrorInformationFile.Close();
                }
                _ = Process.Start(NotepadPath, FatalErrorInformationPath);
            }

            // 終了する｡
            Environment.Exit(1);
        }
    }
}

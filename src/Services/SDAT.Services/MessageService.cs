using SDAT.Services.Interfaces;
using System;
using System.Reflection;

namespace SDAT.Services
{
    public class MessageService : IMessageService
    {
        /// <summary>
        /// バージョン情報取得処理(メインアプリケーション)
        /// </summary>
        /// <returns>メインアプリケーションのバージョン情報</returns>
        public string GetVersionInfoMain()
        {
            return GetVersionInfo("SDAT");
        }

        /// <summary>
        /// バージョン情報取得処理(Core)
        /// </summary>
        /// <returns>Coreのバージョン情報</returns>
        public string GetVersionInfoCore()
        {
            return GetVersionInfo("SDAT.Core");
        }

        /// <summary>
        /// バージョン情報取得処理(Modules.ConvertRadix)
        /// </summary>
        /// <returns>Modules.WelcomeInfoのバージョン情報</returns>
        public string GetVersionInfoModulesConvertRadix()
        {
            return GetVersionInfo("SDAT.Modules.ConvertRadix");
        }

        /// <summary>
        /// バージョン情報取得処理(Modules.WelcomeInfo)
        /// </summary>
        /// <returns>Modules.WelcomeInfoのバージョン情報</returns>
        public string GetVersionInfoModulesWelcomeInfo()
        {
            return GetVersionInfo("SDAT.Modules.WelcomeInfo");
        }

        /// <summary>
        /// バージョン情報取得処理(Modules.AboutInfo)
        /// </summary>
        /// <returns>Modules.AboutInfoのバージョン情報</returns>
        public string GetVersionInfoModulesAboutInfo()
        {
            return GetVersionInfo("SDAT.Modules.AboutInfo");
        }

        /// <summary>
        /// バージョン情報取得処理(Services)
        /// </summary>
        /// <returns>Servicesのバージョン情報</returns>
        public string GetVersionInfoServices()
        {
            return GetVersionInfo("SDAT.Services");
        }

        /// <summary>
        /// バージョン情報取得処理(Services.Interfaces)
        /// </summary>
        /// <returns>Services.Interfaces</returns>
        public string GetVersionInfoServicesInterfaces()
        {
            return GetVersionInfo("SDAT.Services.Interfaces");
        }

        /// <summary>
        /// コピーライト情報取得処理
        /// </summary>
        /// <returns>コピーライト情報</returns>
        public string GetCopyrightInfo()
        {
            // ロードされているアセンブリ情報を検索する
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name == "SDAT")
                {
                    return asm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
                }
            }

            // 見つからなかったら空文字を返す
            return string.Empty;
        }

        /// <summary>
        /// 製品情報取得処理
        /// </summary>
        /// <returns>製品情報</returns>
        public string GetProductInfo()
        {
            // ロードされているアセンブリ情報を検索する
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name == "SDAT")
                {
                    return asm.GetCustomAttribute<AssemblyProductAttribute>().Product;
                }
            }

            // 見つからなかったら空文字を返す
            return string.Empty;
        }

        /// <summary>
        /// バージョン情報取得処理(アセンブリ名指定)
        /// </summary>
        /// <param name="asmName">バージョン情報したいアセンブリの名前</param>
        /// <returns>指定されたアセンブリのバージョン情報</returns>
        private string GetVersionInfo(string asmName)
        {
            // ロードされているアセンブリ情報を検索する
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name == asmName)
                {
                    return asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
                }
            }

            // 見つからなかったら空文字を返す
            return string.Empty;
        }
    }
}

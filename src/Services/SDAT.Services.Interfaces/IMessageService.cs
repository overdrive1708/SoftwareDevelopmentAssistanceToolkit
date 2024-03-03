namespace SDAT.Services.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// バージョン情報取得処理(メインアプリケーション)
        /// </summary>
        /// <returns>メインアプリケーションのバージョン情報</returns>
        string GetVersionInfoMain();

        /// <summary>
        /// バージョン情報取得処理(Core)
        /// </summary>
        /// <returns>Coreのバージョン情報</returns>
        string GetVersionInfoCore();

        /// <summary>
        /// バージョン情報取得処理(Modules.ConvertRadix)
        /// </summary>
        /// <returns>Modules.ConvertRadixのバージョン情報</returns>
        string GetVersionInfoModulesConvertRadix();

        /// <summary>
        /// バージョン情報取得処理(Modules.CompareListItem)
        /// </summary>
        /// <returns>Modules.CompareListItemのバージョン情報</returns>
        string GetVersionInfoModulesCompareListItem();

        /// <summary>
        /// バージョン情報取得処理(Modules.CompareCLangDefine)
        /// </summary>
        /// <returns>Modules.CompareCLangDefineのバージョン情報</returns>
        string GetVersionInfoModulesCompareCLangDefine();

        /// <summary>
        /// バージョン情報取得処理(Modules.WelcomeInfo)
        /// </summary>
        /// <returns>Modules.WelcomeInfoのバージョン情報</returns>
        string GetVersionInfoModulesWelcomeInfo();

        /// <summary>
        /// バージョン情報取得処理(Modules.AboutInfo)
        /// </summary>
        /// <returns>Modules.AboutInfoのバージョン情報</returns>
        string GetVersionInfoModulesAboutInfo();

        /// <summary>
        /// バージョン情報取得処理(Services)
        /// </summary>
        /// <returns>Servicesのバージョン情報</returns>
        string GetVersionInfoServices();

        /// <summary>
        /// バージョン情報取得処理(Services.Interfaces)
        /// </summary>
        /// <returns>Services.Interfaces</returns>
        string GetVersionInfoServicesInterfaces();

        /// <summary>
        /// コピーライト情報取得処理
        /// </summary>
        /// <returns>コピーライト情報</returns>
        string GetCopyrightInfo();

        /// <summary>
        /// 製品情報取得処理
        /// </summary>
        /// <returns>製品情報</returns>
        string GetProductInfo();
    }
}

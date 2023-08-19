namespace SDAT.Services.Interfaces
{
    public interface IConvertRadixService
    {
        /// <summary>
        /// 基数変換処理(2進数から10進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        string ConvertRadixBinToDec(string fromValue);

        /// <summary>
        /// 基数変換処理(2進数から16進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        string ConvertRadixBinToHex(string fromValue);

        /// <summary>
        /// 基数変換処理(10進数から2進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        string ConvertRadixDecToBin(string fromValue);

        /// <summary>
        /// 基数変換処理(10進数から16進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        string ConvertRadixDecToHex(string fromValue);

        /// <summary>
        /// 基数変換処理(16進数から2進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        string ConvertRadixHexToBin(string fromValue);

        /// <summary>
        /// 基数変換処理(16進数から10進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        string ConvertRadixHexToDec(string fromValue);
    }
}

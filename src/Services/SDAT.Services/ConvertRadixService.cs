using SDAT.Services.Interfaces;
using System;

namespace SDAT.Services
{
    public class ConvertRadixService : IConvertRadixService
    {
        /// <summary>
        /// 基数変換処理(2進数から10進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        public string ConvertRadixBinToDec(string fromValue)
        {
            try
            {
                // 32bit整数変換で2進数文字列を10進数にして10進数文字列で返す
                uint decValue = Convert.ToUInt32(fromValue, 2);
                return Convert.ToString(decValue, 10);
            }
            catch
            {
                // 32bit整数変換に失敗する場合は空を返す
                return string.Empty;
            }
        }

        /// <summary>
        /// 基数変換処理(2進数から16進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        public string ConvertRadixBinToHex(string fromValue)
        {
            try
            {
                // 32bit整数変換で2進数文字列を10進数にして16進数文字列で返す
                uint decValue = Convert.ToUInt32(fromValue, 2);
                return Convert.ToString(decValue, 16);
            }
            catch
            {
                // 32bit整数変換に失敗する場合は空を返す
                return string.Empty;
            }
        }

        /// <summary>
        /// 基数変換処理(10進数から2進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        public string ConvertRadixDecToBin(string fromValue)
        {
            try
            {
                // 32bit整数変換で10進数文字列を10進数にして2進数文字列で返す
                uint decValue = Convert.ToUInt32(fromValue, 10);
                return Convert.ToString(decValue, 2);
            }
            catch
            {
                // 32bit整数変換に失敗する場合は空を返す
                return string.Empty;
            }
        }

        /// <summary>
        /// 基数変換処理(10進数から16進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        public string ConvertRadixDecToHex(string fromValue)
        {
            try
            {
                // 32bit整数変換で10進数文字列を10進数にして16進数文字列で返す
                uint decValue = Convert.ToUInt32(fromValue, 10);
                return Convert.ToString(decValue, 16);
            }
            catch
            {
                // 32bit整数変換に失敗する場合は空を返す
                return string.Empty;
            }
        }

        /// <summary>
        /// 基数変換処理(16進数から2進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        public string ConvertRadixHexToBin(string fromValue)
        {
            try
            {
                // 32bit整数変換で16進数文字列を10進数にして2進数文字列で返す
                uint decValue = Convert.ToUInt32(fromValue, 16);
                return Convert.ToString(decValue, 2);
            }
            catch
            {
                // 32bit整数変換に失敗する場合は空を返す
                return string.Empty;
            }
        }

        /// <summary>
        /// 基数変換処理(16進数から10進数)
        /// </summary>
        /// <param name="fromValue">変換元数値</param>
        /// <returns>変換後数値</returns>
        public string ConvertRadixHexToDec(string fromValue)
        {
            try
            {
                // 32bit整数変換で16進数文字列を10進数にして10進数文字列で返す
                uint decValue = Convert.ToUInt32(fromValue, 16);
                return Convert.ToString(decValue, 10);
            }
            catch
            {
                // 32bit整数変換に失敗する場合は空を返す
                return string.Empty;
            }
        }
    }
}

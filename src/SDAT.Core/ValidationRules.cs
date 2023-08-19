using System;
using System.Globalization;
using System.Windows.Controls;

namespace SDAT.Core
{
    /// <summary>
    /// 入力値検証クラス(数値：基数)
    /// </summary>
    public class NumberRadixValidationRule : ValidationRule
    {
        /// <summary>
        /// 文字列基数
        /// </summary>
        public StringRadixes StringRadix { get; set; }
        public enum StringRadixes
        {
            Binary,
            Decimal,
            Hexadecimal
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public void StringValidationRules()
        {
            StringRadix = StringRadixes.Decimal;
        }

        /// <summary>
        /// 入力値検証処理
        /// </summary>
        /// <param name="value">入力値</param>
        /// <param name="cultureInfo">カルチャー情報</param>
        /// <returns>入力値検証結果</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 32bit整数変換用基数判定
            int convertBase = StringRadix switch
            {
                StringRadixes.Binary => 2,
                StringRadixes.Decimal => 10,
                StringRadixes.Hexadecimal => 16,
                _ => 10,
            };

            // 32bit整数変換を基数指定で行い､例外が発生する場合は基数が不正と判断する｡
            try
            {
                _ = Convert.ToUInt32(value.ToString(), convertBase);
            }
            catch
            {
                // 32bit整数変換に失敗する場合はNGを返す
                ValidationResult validationResult = StringRadix switch
                {
                    StringRadixes.Binary => new ValidationResult(false, Resources.Strings.ValidationErrorNotBinary),
                    StringRadixes.Decimal => new ValidationResult(false, Resources.Strings.ValidationErrorNotDecimal),
                    StringRadixes.Hexadecimal => new ValidationResult(false, Resources.Strings.ValidationErrorNotHexadecimal),
                    _ => new ValidationResult(false, Resources.Strings.ValidationErrorNotBinary),
                };
                return validationResult;
            }

            // 上記のチェックにパスしたらOKを返す
            return ValidationResult.ValidResult;
        }
    }
}

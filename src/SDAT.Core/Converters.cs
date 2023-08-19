using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SDAT.Core
{
    /// <summary>
    /// コンバータ(全てFALSEかどうか)
    /// </summary>
    public class IsAllFalseConverter : IMultiValueConverter
    {
        /// <summary>
        /// 変換処理
        /// </summary>
        /// <param name="values">変換対象</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>変換結果</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.OfType<bool>().All(x => !x);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.Data.FitsInfo;
using MossbauerLab.UnivemMsAggr.Core.Data.FitsInfo.CalculatedValues;

namespace MossbauerLab.UnivemMsAggr.Core.UnivemMs.FilesProcessor
{
    public static class CompProcessor
    {
        /// <summary>
        ///     These files encoding is CP-1251
        /// </summary>
        /// <param name="componentsFile"></param>
        /// <returns></returns>
        public static SpectrumFit Process(String componentsFile)
        {
            SpectrumFit fit = new SpectrumFit();
            if (!File.Exists(Path.GetFullPath(componentsFile)))
                return null;
            IList<String> content = File.ReadAllLines(Path.GetFullPath(componentsFile), Encoding.GetEncoding(CompFilesCodePage));
            if (!content.Any(line => String.Equals(line, ComponentsFileSign)))
                return null;
            UInt16 cahnnelsNumber = GetValue<UInt16>(content, ChannelsNumberKey, ChannelsNumberPattern);
            Decimal velocityStep = GetValue<Decimal>(content, VelocityStepKey, VelocityStepLinePattern);
            Decimal chiSquareValue = GetValue<Decimal>(content, ChiSquareKey,ChiSquareLinePattern);
            fit.Info = new ComponentsInfo(chiSquareValue, velocityStep, EmpiricCalculations.CalculateHyperfineFieldError(velocityStep), cahnnelsNumber);
            return fit;
        }

        private static T GetValue<T>(IList<String> fileContent, String key, String pattern) where T : IConvertible
        {
            String selectedLine = null;
            foreach (String line in fileContent)
            {
                if (Regex.Match(line, pattern).Success)
                {
                    selectedLine = line;
                    break;
                }
            }
            if (selectedLine == null)
                return default(T);
            Int32 index = selectedLine.IndexOf(key, StringComparison.InvariantCulture);
            if(index < 0)
                return default(T);
            index += key.Length;
            String residual = selectedLine.Substring(index).Trim();
            String[] parts = residual.Split(' ');
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";
            return (T)Convert.ChangeType(parts[0], typeof(T), format);
        }

        private const String CompFilesCodePage = "Windows-1251";

        private const String ComponentsFileSign = "Исп.единицы: Is,G,Qs - мм/с; H - кЭ; A - %; отн.G,A - б/разм.";
        private const String VelocityStepKey = "Цена канала: ";
        private const String VelocityStepLinePattern = @"^[\w\W]*" + VelocityStepKey + @"[\w\W]*$";
        private const String ChiSquareKey = "Xi_2 =";
        private const String ChiSquareLinePattern = @"^" + ChiSquareKey + @"[\w\W]*$";
        private const String ChannelsNumberKey = "Точек подгонки (интервалы):";
        private const String ChannelsNumberPattern = @"^Точек подгонки \(интервалы\):[\w\W]*$";
    }
}

using System.Globalization;
using System.Xml.Linq;

namespace ArincConverter.Helpers
{
    public static class Extensions
    {
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string WithSpaces(this string value)
        {
            return string.Concat(value.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        public static string NormalizeSentence(this string value)
        {
            return string.Concat(value?.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ').ToUpper();
        }

        public static object GetPropertyValue(this Type type, string name, object obj)
        {
            var parts = name.Split('.').ToList();
            var currentPart = parts[0];
            var info = type.GetProperty(currentPart);
            if (info == null || obj == null) return null;
            if (name.IndexOf(".") > -1)
                return info.PropertyType.GetPropertyValue(parts[1], info.GetValue(obj, null));
            return info.GetValue(obj, null);
        }

        public static DateTime ToUniversalTime(this string date, string format)
        {
            DateTime parsed = default;
            if (DateTime.TryParse(date, out var realDueDate)) parsed = realDueDate;

            return DateTime.ParseExact(parsed.ToString(format, CultureInfo.InvariantCulture), format, null);
        }

        public static string AppendZero(this string value, int length)
        {
            while (value.Length < length) value = $"0{value}";

            return value;
        }

        public static TimeSpan ToTimeSpan(this string time)
        {
            var hFrom = time?.IndexOf("PT") + "PT".Length ?? 0;
            var hTo = time?.LastIndexOf("H") ?? 0;
            hTo = hTo == -1 ? 0 : hTo;

            var mFrom = time?.IndexOf("H") + "H".Length ?? 0;
            mFrom = mFrom == 0 ? hFrom : mFrom;
            var mTo = time?.LastIndexOf("M") ?? 0;
            mTo = mTo == -1 ? 0 : mTo;

            var sFrom = time?.IndexOf("M") + "M".Length ?? 0;
            sFrom = sFrom == 0 ? mFrom == 0 ? hFrom : mFrom : sFrom;
            var sTo = time?.LastIndexOf("S") ?? 0;
            sTo = sTo == -1 ? 0 : sTo;

            var hour = hTo > hFrom ? Convert.ToInt32(time?.Substring(hFrom, hTo - hFrom)) : 0;
            var min = mTo > mFrom ? Convert.ToInt32(time?.Substring(mFrom, mTo - mFrom)) : 0;
            var sec = sTo > sFrom ? Convert.ToInt32(time?.Substring(sFrom, sTo - sFrom).Replace(".", "")) : 0;

            return new TimeSpan(hour, min, sec);
        }

        public static double TransformCoordinates(this string coordinate)
        {
            if (double.TryParse(coordinate, out double coord))
            {
                return coord;
            }

            return 0;
        }

        public static bool IsValidPath(this string path, bool allowRelativePaths = false)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                {
                    isValid = Path.IsPathRooted(path);
                }
                else
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }

        public static double ConvertToNVDFuelFormat(this string value)
        {
            if (value.Trim() != "")
            {
                while (value.Length < 4) value = "0" + value;
                if (value.Length > 4)
                    value = value.Insert(2, ".");
                else
                    value = value.Insert(1, ".");

                return Convert.ToDouble(value);
            }

            return Convert.ToDouble(value);
        }

        private static object GetXElement(object item)
        {
            if (item != null && item.GetType() == typeof(Dictionary<string, object>))
            {
                IList<XElement> xElements = new List<XElement>();
                foreach (var item2 in item as Dictionary<string, object>)
                    xElements.Add(new XElement(item2.Key, GetXElement(item2.Value)));

                return xElements.ToArray();
            }

            return item;
        }
    }
}

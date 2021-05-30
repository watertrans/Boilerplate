using System;
using System.Collections.Generic;
using System.Text;
using WaterTrans.Boilerplate.Domain;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Persistence
{
    public static class DataUtil
    {
        public static string EscapeLike(string value, LikeMatchType matchType)
        {
            if (value == null)
            {
                return value;
            }

            value = value.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_");

            switch (matchType)
            {
                case LikeMatchType.PrefixSearch:
                    return value + "%";
                case LikeMatchType.PartialMatch:
                    return "%" + value + "%";
                case LikeMatchType.SuffixSearch:
                    return "%" + value;
                default:
                    return value;
            }
        }

        public static string ConvertToOrderBy(SortOrder sortOrder, params string[] columnWhiteList)
        {
            var hash = new HashSet<string>(columnWhiteList, StringComparer.OrdinalIgnoreCase);
            var result = new StringBuilder();
            foreach (var item in sortOrder)
            {
                if (hash.Contains(item.Field))
                {
                    result.Append(" `" + item.Field.Replace("`", "``") + "` " + item.SortType.ToString() + ",");
                }
            }

            if (result.Length == 0)
            {
                return string.Empty;
            }

            result.Length -= 1;
            return " ORDER BY " + result.ToString();
        }
    }
}

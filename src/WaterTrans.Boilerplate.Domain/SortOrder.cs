using System.Collections.Generic;

namespace WaterTrans.Boilerplate.Domain
{
    public class SortOrder : List<SortOrderItem>
    {
        public static SortOrder Parse(string sort)
        {
            var result = new SortOrder();

            if (string.IsNullOrEmpty(sort))
            {
                return result;
            }

            foreach (string sortItem in sort.Split(','))
            {
                var item = new SortOrderItem();

                string sortItemTrim = sortItem.Trim();
                if (sortItemTrim.StartsWith("-"))
                {
                    item.Field = sortItemTrim.Substring(1).Trim();
                    item.SortType = SortType.DESC;
                }
                else
                {
                    item.Field = sortItemTrim;
                    item.SortType = SortType.ASC;
                }

                result.Add(item);
            }

            return result;
        }
    }
}

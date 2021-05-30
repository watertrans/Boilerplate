using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Resources;

namespace WaterTrans.Boilerplate.Domain.ValueObjects
{
    public class City : ValueObject
    {
        private static readonly Dictionary<string, Dictionary<string, object>>
            s_cities = new Dictionary<string, Dictionary<string, object>>
        {
            { "TYO", new Dictionary<string, object> { { "CityCode", "TYO" }, { "Name", "TOKYO" } } },
            { "OSA", new Dictionary<string, object> { { "CityCode", "OSA" }, { "Name", "OSAKA" } } },
            { "FUK", new Dictionary<string, object> { { "CityCode", "FUK" }, { "Name", "FUKUOKA" } } },
            { "BOS", new Dictionary<string, object> { { "CityCode", "BOS" }, { "Name", "BOSTON" } } },
            { "CHI", new Dictionary<string, object> { { "CityCode", "CHI" }, { "Name", "CHICAGO" } } },
            { "NYC", new Dictionary<string, object> { { "CityCode", "NYC" }, { "Name", "NEW YORK CITY" } } },
        };

        public City(string code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (!s_cities.ContainsKey(code)) throw new ArgumentException(string.Format(ErrorMessages.NotContains, code, nameof(code)));
            var item = s_cities[code];
            CityCode = (string)item["CityCode"];
            Name = (string)item["Name"];
        }

        public string CityCode { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Resources;

namespace WaterTrans.Boilerplate.Domain.ValueObjects
{
    public class Country : ValueObject
    {
        private static readonly Dictionary<string, Dictionary<string, object>>
            s_countries = new Dictionary<string, Dictionary<string, object>>
        {
            { "JPN", new Dictionary<string, object> { { "CountryCode", "JPN" }, { "Name", "JAPAN" } } },
            { "USA", new Dictionary<string, object> { { "CountryCode", "USA" }, { "Name", "U.S.A." } } },
        };

        public Country(string code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (!s_countries.ContainsKey(code)) throw new ArgumentException(string.Format(ErrorMessages.NotContains, code, nameof(code)));
            var item = s_countries[code];
            CountryCode = (string)item["CountryCode"];
            Name = (string)item["Name"];
        }

        public string CountryCode { get; }
        public string Name { get; }
    }
}

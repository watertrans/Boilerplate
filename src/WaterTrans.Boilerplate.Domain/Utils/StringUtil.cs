﻿using System;

namespace WaterTrans.Boilerplate.Domain.Utils
{
    public static class StringUtil
    {
        public static string ToCamelCase(this string value)
        {
            if (value == null || value.Length == 0)
            {
                return value;
            }
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        public static string CreateCode()
        {
            return (StringUtil.Base64UrlEncode(Guid.NewGuid().ToByteArray()) +
                    StringUtil.Base64UrlEncode(Guid.NewGuid().ToByteArray()))
                        .Replace("-", string.Empty)
                        .Replace("_", string.Empty);
        }

        public static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public static byte[] Base64UrlDecode(string input)
        {
            string incoming = input.Replace('_', '/').Replace('-', '+');
            switch (input.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }
            return Convert.FromBase64String(incoming);
        }
    }
}

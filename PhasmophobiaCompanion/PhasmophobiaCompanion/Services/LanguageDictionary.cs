using System;
using System.Collections.Generic;
using System.Linq;

namespace PhasmophobiaCompanion.Services
{
    public static class LanguageDictionary
    {
        public static readonly Dictionary<string, string> LanguageMap = new Dictionary<string, string>
        {
            {"English", "EN"},
            {"Русский", "RU"}
        };

        public static string GetLanguageNameByCode(string code)
        {
            // Ищем в словаре первую пару, значение которой соответствует заданному коду
            var languageEntry =
                LanguageMap.FirstOrDefault(x => x.Value.Equals(code, StringComparison.OrdinalIgnoreCase));

            // Если такая пара найдена, возвращаем ключ, иначе null или любое другое указанное значение
            return languageEntry.Key ?? null;
        }
    }
}
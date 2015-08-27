using System;
using System.Linq;

namespace Helpers
{
    public static class GetCollectionStringName
    {
        private const char CharacterOLiteral = 'o';
        private const char CharacterYLiteral = 'y';

        private static readonly char[] vowels = new char[]
        {
            'a', 'e', 'i', 'o', 'y', 'u'
        };

        public static string GetCollectionName<T>()
        {
            Type collectionType = typeof(T);
            string collectionName = collectionType.Name.ToLower();
            return DetermineCollectioName(collectionName);
        }

        private static string DetermineCollectioName(string typeName)
        {
            char lastTypeNameLetter = typeName[typeName.Length - 1];
            string currentWord;
            switch (lastTypeNameLetter)
            {
                case 'o':
                    currentWord = WordsFinishingInO(typeName);
                    break;
                case 'y':
                    currentWord = WordsFinishingInY(typeName);
                    break;
                default:
                    currentWord = StandardWordPlural(typeName);
                    break;
            }

            return currentWord;
        }

        private static string WordsFinishingInY(string typeName)
        {
            bool hasVowelBeforeY = vowels.Contains(typeName[typeName.Length - 2]);
            if (hasVowelBeforeY)
            {
                return StandardWordPlural(typeName);
            }

            string baseWord = typeName.Substring(0, typeName.Length - 1);
            return baseWord + "ies";
        }

        private static string WordsFinishingInO(string typeName)
        {
            return typeName + "es";
        }

        private static string StandardWordPlural(string typeName)
        {
            return typeName + 's';
        }
    }
}

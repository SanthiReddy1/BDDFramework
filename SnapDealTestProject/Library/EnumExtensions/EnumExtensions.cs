using System.Collections.Generic;

namespace SnapDealTestProject.Library.EnumExtensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using SnapDealTestProject.Utils.Enum;

    public static class EnumExtensions
    {
        public static readonly Dictionary<SearchSortOptions, string> EnumValues = new Dictionary<SearchSortOptions, string>();

        /*public static string Value(this SearchSortOptions theEnum, string value)
        {
            
            *//*if (EnumValues.ContainsKey(theEnum))
                return EnumValues[theEnum];

            EnumMemberAttribute memberAttribute = EnumMemberAttribute.EnumMemberAttributeOf(theEnum);
            if (!EnumValues.ContainsKey(theEnum))
            {
                EnumValues.Add(theEnum, memberAttribute.Value);
            }
            return EnumValues[theEnum];*//*
        }*/

        public static string GetNameAttribute(this SearchSortOptions sortOptions)
        {
            Type deviceStatusType = typeof(SearchSortOptions);
            string sortOptionName = Enum.GetName(deviceStatusType, sortOptions);
            MemberInfo[] memberInfo = deviceStatusType.GetMember(sortOptionName);

            if (memberInfo.Length != 1)
            {
                throw new ArgumentException($"DeviceStatus of {sortOptionName} should only have one memberInfo");
            }

            IEnumerable<LocatorAttribute> customAttributes = memberInfo[0].GetCustomAttributes<LocatorAttribute>();
            LocatorAttribute nameAttribute = customAttributes.FirstOrDefault();

            if (nameAttribute == null)
            {
                throw new InvalidOperationException($"DeviceStatus of {sortOptionName} has no BackgroundColorAttribute");
            }

            return nameAttribute.Name;
        }

        private static T GetAttribute<T>(this SearchSortOptions sortOptions)
            where T : System.Attribute
        {
            return (sortOptions.GetType().GetMember(Enum.GetName(sortOptions.GetType(), sortOptions))[0].GetCustomAttributes(typeof(T), inherit: false)[0] as T);
        }

        public static string GetBgColor(this SearchSortOptions sortOptions)
        {
            return sortOptions.GetAttribute<LocatorAttribute>().Name;
        }

        public static string GetEnumValue(this Enum enumValue)
        {
            string enumText = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault().GetCustomAttribute<LocatorAttribute>(false).Name;

            if (string.IsNullOrEmpty(enumText))
            {
                enumText = enumValue.ToString();
            }

            return enumText;
        }
    }
}
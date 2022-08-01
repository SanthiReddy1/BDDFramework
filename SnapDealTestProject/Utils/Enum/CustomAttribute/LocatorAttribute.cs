namespace SnapDealTestProject.Utils.Enum
{
    using System;

    public class LocatorAttribute : Attribute
    {
        public string Name;

        public LocatorAttribute(string name)
        {
            this.Name = name;
        }
    }
}

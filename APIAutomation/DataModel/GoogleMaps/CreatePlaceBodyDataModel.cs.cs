namespace APIAutomation.DataModel
{
    using System.Collections.Generic;

    public class CreatePlaceBodyDataModel
    {
        public LocationDataModel location { get; set; }

        public int accuracy { get; set; }

        public string name { get; set; }

        public string phone_number { get; set; }

        public string address { get; set; }

        public List<string> types { get; set; }
    
        public string website { get; set; }

        public string language { get; set; }
    }
}

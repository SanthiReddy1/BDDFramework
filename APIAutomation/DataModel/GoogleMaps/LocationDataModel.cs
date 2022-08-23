namespace APIAutomation.DataModel
{
    public class LocationDataModel
    {
        public double lat { get; set; }

        public double lng { get; set; }

        public LocationDataModel(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }
}

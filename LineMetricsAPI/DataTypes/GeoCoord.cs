using System.Runtime.Serialization;

namespace LineMetrics.API.DataTypes
{
    [DataContract]
    public class GeoCoord : Base
    {
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        [DataMember(Name = "long")]
        public double Longitude { get; set; }

        [DataMember(Name = "precision")]
        public int Precision { get; set; }

        public override string ToString()
        {
           // return string.Format("Latitude: {0}, Longitude: {1}, Timestamp: {2:dd.MM.yyy HH:mm:ss}", Latitude, Longitude, Timestamp);
            return string.Format("{0:dd.MM.yyy HH:mm:ss};{1};{2}", Timestamp, Latitude, Longitude).Replace(",",".");
        }
    }
}

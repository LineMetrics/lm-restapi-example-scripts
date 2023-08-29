using System.Runtime.Serialization;

namespace LineMetrics.API.DataTypes
{
    [DataContract]
    public class Double : Base
    {
        [DataMember(Name = "val")]
        public double? Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0:dd.MM.yyy HH:mm:ss};{1}", Timestamp, Value).Replace(",", ".");
            //return string.Format("Value: {0}, Timestamp: {1:dd.MM.yyy HH:mm:ss}", Value, Timestamp);
        }
    }
}

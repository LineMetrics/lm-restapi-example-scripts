using System.Runtime.Serialization;

namespace LineMetrics.API.DataTypes
{
    [DataContract]
    public class String : Base
    {
        [DataMember(Name = "val")]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0:dd.MM.yyy HH:mm:ss};{1}", Timestamp, Value);
            //return string.Format("Value: {0}, Timestamp: {1:dd.MM.yyy HH:mm:ss}", Value, Timestamp);
        }
    }
}

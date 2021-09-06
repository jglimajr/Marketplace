using System.Text.Json.Serialization;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.Utils.Globals.Classes
{
    public class Return
    {
        public Return(ReturnValues status, dynamic value)
        {
            this.Status = status;
            this.Value = value;
        }
        public ReturnValues Status { get; private set; }
        public dynamic Value { get; private set; }
        [JsonIgnore]
        public int Record { get; private set; }
        [JsonIgnore]
        public int TotalPages { get; private set; }
    }
}
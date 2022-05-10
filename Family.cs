using Newtonsoft.Json;
using System.Collections.Generic;

namespace CosmosGettingStartedTutorial
{
    public class Family
    {
        public status status { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class status {
        public string rtsCaptureStatus { get; set; }
        public string controlProcessStatus { get; set; }
        public string initialStatus { get; set; }
        public string transformedOrderStatus { get; set; }
        public string lightningCartStatus { get; set; }
        public string vertexStatus { get; set; }
    }
    
}



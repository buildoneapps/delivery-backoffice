using System;
using System.Collections.Generic;

namespace delivery.backoffice.API.Model.Proxy
{
    public class DriverProxy
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public int DeliveryCount { get; set; }
        
        public bool IsBlocked { get; set; }
        
        public double Rate { get; set; }
        
        public string RegisterDate { get; set; }
        
        public string Level { get; set; }
        
        public List<DocumentProxy> Documents { get; set; }
        
        public List<string> Vehicles { get; set; }
        
        public int TotalDeliveries { get; set; }
        
        public double TotalDeliveriesMoney { get; set; }
        
        public double TotalDeliveriesToPay { get; set; }
    }
}
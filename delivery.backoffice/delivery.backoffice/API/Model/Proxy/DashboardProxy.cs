namespace delivery.backoffice.API.Model.Proxy
{
    public class DashboardProxy
    {
        public int Opened { get; set; }
        
        public int Executing { get; set; }
        
        public int Finished { get; set; }
        
        public int NotDelivered { get; set; }
    }
}
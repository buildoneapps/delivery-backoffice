using System;

namespace delivery.backoffice.API.Model.Proxy
{
    public class AppTopicProxy
    {
        public Guid Id { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Type { get; set; }
    }
}
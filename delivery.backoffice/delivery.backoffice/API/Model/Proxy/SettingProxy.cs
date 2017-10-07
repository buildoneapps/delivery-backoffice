using System;

namespace delivery.backoffice.API.Model.Proxy
{
    public class SettingProxy
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Value { get; set; }
        
        public string Key { get; set; }
        
        public string SubKey { get; set; }
        
        public int Type { get; set; }
    }
}
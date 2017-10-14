using System;
using System.ComponentModel;

namespace delivery.backoffice.API.Model.Proxy
{
    public class SettingProxy
    {
        public Guid Id { get; set; }
        
        [DisplayName("Nome")]
        public string Name { get; set; }
        
        [DisplayName("Valor")]
        public string Value { get; set; }
        
        public string Key { get; set; }
        
        public string SubKey { get; set; }
        
        public int Type { get; set; }
    }
}
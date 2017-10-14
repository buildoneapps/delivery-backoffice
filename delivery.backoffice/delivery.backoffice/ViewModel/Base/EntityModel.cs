using System;

namespace delivery.backoffice.ViewModel
{
    public class EntityModel
    {
        public Guid Id { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
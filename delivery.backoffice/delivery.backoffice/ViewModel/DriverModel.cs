using System;
using System.ComponentModel.DataAnnotations;

namespace delivery.backoffice.ViewModel
{
    public class DriverModel : EntityModel
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
        
        public int YearsOld { get; set; }
        
        public string Gender { get; set; }
        
        public int DeliveryCount { get; set; }
        
        public int PaymentMethod { get; set; }
        
        public double Rating { get; set; }
        
        public int CountRating { get; set; }
        
        public bool IsBlocked { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public string BlockedReason { get; set; }
        
        public DateTime RegisterDate { get; set; }
        
        public DateTime? ExpireDate { get; set; }
        
        public DateTime? ReleaseDate { get; set; }

        public bool IsAvailableForTravel { get; set; }
        
        public int Level { get; set; }

        public double Lat { get; set; }
        
        public double Lon { get; set; }
        
        public double ToPay { get; set; }
        
        public string CEP { get; set; }
        
        public string Address { get; set; }
        
        public string Neighborhood { get; set; }
        
        public string Number { get; set; }
        
        public string Complement { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public bool AcceptCreditDebitCard { get; set; }
    }
}
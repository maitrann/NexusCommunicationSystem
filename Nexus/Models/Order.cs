﻿using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Order")]
    public class Order
    {
        
        public int Id { get; set; }
        public int ServicePlanID { get; set; }
        [ForeignKey("ServicePlanID")]
        public ServicePlan OrderServicePlan { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer OrderCustomer { get; set; }
        public int ExpiryDateID { get; set; }
        [ForeignKey("ExpiryDateID")]
        public ExpiryDate OrderExpiryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public decimal TheAmountPaid { get; set; }
        public decimal Total { get; set; }
        public bool StatusPaid { get; set; }
        public int StatusService { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ET.Models.DataBase.Order
{
    public class Order
    {
        [Required]
        public Guid OrderId {  get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public Guid BusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}

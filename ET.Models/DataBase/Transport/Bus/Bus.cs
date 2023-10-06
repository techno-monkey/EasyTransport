using System.ComponentModel.DataAnnotations;

namespace ET.Models.DataBase.Transport.Bus
{
    public class Bus
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid TransporterId { get; set; }
        [Required]
        public double Fare { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
    }
}

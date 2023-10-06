using System.ComponentModel.DataAnnotations;

namespace ET.Models.DataBase.Transport.Bus
{
    public class Transporter
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}

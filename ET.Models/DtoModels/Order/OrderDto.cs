using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Models.DtoModels.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BusId { get; set; }
        public string BusName { get; set; }
        public double BusRate { get; set; }

    }
}

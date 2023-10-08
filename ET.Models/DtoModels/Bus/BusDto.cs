namespace ET.Models.DtoModels.Bus
{
    public class BusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TransporterName { get; set; }
        public Guid TransporterId { get; set; }
        public double Fare { get; set; }
    }
}

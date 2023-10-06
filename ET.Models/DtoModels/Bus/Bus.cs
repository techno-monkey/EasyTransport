namespace ET.Models.DtoModels.Bus
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TransporterName { get; set; }
        public double Fare { get; set; }
    }
}

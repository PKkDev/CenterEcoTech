namespace CenterEcoTech.Domain.DTO.MeasurementRequest
{
    public class LastCounterDto
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public LastCounterDto(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}

namespace CenterEcoTech.Domain.DTO.MeasurementRequest
{
    public class LastCounterDto
    {
        public string Name { get; set; }

        public double? Value { get; set; }

        public string? Postfix { get; set; }

        public LastCounterDto(string name, double? value, string? postfix)
        {
            Name = name;
            Value = value;
            Postfix = postfix;
        }
    }
}

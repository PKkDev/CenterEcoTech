namespace CenterEcoTech.Domain.DTO.MeasurementRequest
{
    public class MeasurementRequestDto
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public string CLientName { get; set; }

        public string CLientAdress { get; set; }

        public string? Postfix { get; set; }
    }
}

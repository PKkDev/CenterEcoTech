namespace CenterEcoTech.Domain.DTO.Сooperative
{
    public class СooperativeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public СooperativeDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}

using Atlas.Domain.Enum;

namespace Atlas.API.DTO
{
    public class RefinitivWorldCheckScreeningDto
    {
        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Country Nationality { get; set; }
    }
}

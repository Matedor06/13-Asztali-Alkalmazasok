

namespace REST_Sample_01.Models
{
    public class MotorcycleUpdateModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]

        public string Name { get; set; }
    }
}

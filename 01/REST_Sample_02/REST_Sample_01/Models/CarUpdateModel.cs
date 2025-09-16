

namespace REST_Sample_01.Models
{
    public class CarUpdateModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]

        public string Name { get; set; }
    }
}

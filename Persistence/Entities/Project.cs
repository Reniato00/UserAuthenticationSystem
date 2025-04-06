namespace AuthenticationSystemApi.Entities
{
    public class Project
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }

        public string? Description { get; set; }

        public required int Security { get; set; }
    }
}

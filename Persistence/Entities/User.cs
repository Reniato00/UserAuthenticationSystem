namespace AuthenticationSystemApi.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public string? Phone { get; set; }

        public required string ProjectId { get; set; }

        public Project? Project { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Active { get; set; } = false;
    }
}

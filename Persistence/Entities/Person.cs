namespace AuthenticationSystemApi.Entities
{
    public class Person
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public required string UserId { get; set; }

        public required User User { get; set; }
    }
}

using AuthenticationSystemApi.Entities;
using Persistence.Repositories;

namespace AuthenticationSystemApi.Services
{
    public interface IPersonServices
    {
        Task<Person?> GetPerson(string userId);
    }

    public class PersonServices : IPersonServices
    {
        private readonly IUserSystemDbRepository db;
        public PersonServices(IUserSystemDbRepository db)
        {
            this.db = db;
        }

        public Task<Person?> GetPerson(string userId)
        {
            var person = db.GetPerson(userId);
            return person;  
        }
    }
}

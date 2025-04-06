using AuthenticationSystemApi.Entities;
using Persistence.Entities.Requests;
using Persistence.Repositories;

namespace AuthenticationSystemApi.Services
{
    public interface IUserServices
    {
        Task<User?> GetUser(Credentials credentials, string projectId);
        Task<bool> CreateUser(User user);
        Task<List<User>> GetUsers(int skip, int take);
    }
    public class UserServices : IUserServices
    {
        private readonly IUserSystemDbRepository db;
        public UserServices(IUserSystemDbRepository db) 
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<User?> GetUser(Credentials credentials, string projectId)
        {
            return await db.GetUser(projectId, credentials.UserName, credentials.Password);
        }

        public async Task<List<User>> GetUsers(int skip, int take)
        {
            return await db.GetUsers(skip, take);
        }

        public async Task<bool> CreateUser(User user)
        {
            return await db.CreateUser(user);
        }
    }
}

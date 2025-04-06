using AuthenticationSystemApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public interface IUserSystemDbRepository
    {
        Task<Person?> GetPerson(string userId);
        Task<User?> GetUser(string projectId, string username, string password);
        Task<bool> CreateProject(Project project);
        Task<List<Project>> GetProjects(int skip, int take);
        Task<bool> CreateUser(User user);
        Task<List<User>> GetUsers(int skip, int take);
    }

    public class UserSystemDbRepository : IUserSystemDbRepository
    {
        private readonly Context context;
        public UserSystemDbRepository(Context context)
        {
            this.context = context;
        }

        public async Task<Person?> GetPerson(string userId)
        {
            return await context.Persons.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User?> GetUser(string projectId, string username, string password)
        {
            return await context.Users.FirstOrDefaultAsync(x => 
            x.ProjectId == projectId && 
            x.Name == username && 
            x.Password == password
            );
        }

        public async Task<List<User>> GetUsers(int skip, int take)
        {
            return await context.Users.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<bool> CreateUser(User user)
        {
            await context.Users.AddAsync(user);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateProject(Project project)
        {
            await context.Projects.AddAsync(project);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<Project>> GetProjects(int skip, int take)
        {
            return await context.Projects.Skip(skip).Take(take).ToListAsync();
        }
    }
}

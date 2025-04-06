using AuthenticationSystemApi.Entities;
using Persistence.Repositories;

namespace AuthenticationSystemApi.Services
{
    public interface IProjectServices
    {
        Task<bool> CreateProject(Project project );
        Task<List<Project>> GetProjects(int skip, int take);
    }
    public class ProjectServices : IProjectServices
    {
        private readonly IUserSystemDbRepository db;
        public ProjectServices(IUserSystemDbRepository db) 
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<bool> CreateProject(Project project)
        {
            return await db.CreateProject(project);
        }

        public async Task<List<Project>> GetProjects(int skip, int take)
        {
            return await db.GetProjects(skip, take);
        }
    }
}

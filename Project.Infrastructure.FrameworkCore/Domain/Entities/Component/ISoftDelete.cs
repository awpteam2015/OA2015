namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Component
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}

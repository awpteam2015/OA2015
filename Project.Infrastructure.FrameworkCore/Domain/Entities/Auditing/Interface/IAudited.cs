namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface
{
    /// <summary>
    /// 创建和修改权限
    /// </summary>
    public interface IAudited :ICreationAudited,IModificationAudited
    {

    }
}

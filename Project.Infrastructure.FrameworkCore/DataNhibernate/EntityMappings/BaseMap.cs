using FluentNHibernate.Mapping;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings
{

    public class BaseMap<TEntity, TPrimaryKey> : ClassMap<TEntity> where TEntity : Entity<TPrimaryKey>
    {
        protected BaseMap(string tableName)
        {
            Table(tableName);
            Id(p => p.PkId);
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Where("IsDeleted = 0"); //TODO: Test with other DBMS then SQL Server
            }
            DynamicInsert();
            DynamicUpdate();
        }
    }



    public class FullMap<TEntity, TPrimaryKey> : BaseMap<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>, IHasCreationTime, ISoftDelete
    {
        protected FullMap(string tableName)
            : base(tableName)
        {
            Id(p => p.PkId).GeneratedBy.Identity();
            this.MapCreationTime();
            this.MapIsDeleted();
        }
    }

    /// <summary>
    /// 视图的映射基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewBaseMap<T> : ClassMap<T> where T : class
    {
        protected ViewBaseMap(string viewName)
        {
            ReadOnly();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(Entity)))
            {
                Where("IsDeleted = 0"); //TODO: Test with other DBMS then SQL Server
            }
        }
    }





}

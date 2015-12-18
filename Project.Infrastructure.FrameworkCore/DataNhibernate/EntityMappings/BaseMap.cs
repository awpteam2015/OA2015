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

}

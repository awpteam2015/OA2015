using FluentNHibernate.Data;
using FluentNHibernate.Mapping;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings
{
    public static class NhMappingExtensions
    {

        public static void MapCreationTime<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IHasCreationTime
        {
            mapping.Map(x => x.CreationTime);
        }


        public static void MapIsDeleted<TEntity>(this ClassMap<TEntity> mapping) where TEntity : ISoftDelete
        {
            mapping.Map(x => x.IsDeleted);
        }

        public static void MapPkidDefault<TEntity, TPrimaryKey>(this ClassMap<TEntity> mapping) where TEntity : IEntity<TPrimaryKey>
        {
            mapping.Id(p => p.PkId).GeneratedBy.Identity();
        }
    }
}

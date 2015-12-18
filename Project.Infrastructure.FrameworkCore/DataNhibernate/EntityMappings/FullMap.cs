using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings
{
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
}

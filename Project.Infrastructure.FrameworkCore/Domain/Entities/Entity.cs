using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
    public interface  IEntity<TId>
    {
          TId PkId { get;  set; }
          int Version { get; set; }
    }

    [Serializable]
    public abstract class Entity<TId> : IEntity<TId>
    {   
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj != null &&
                   Equals(obj.PkId, default(TId));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(PkId, other.PkId))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(PkId, default(TId)))
                return base.GetHashCode();
            return PkId.GetHashCode();
        }

        public virtual TId PkId { get; set; }
        public virtual int Version { get; set; }
    }

    [Serializable]
    public abstract class Entity : Entity<int>
    {

    }

    [Serializable]
    public abstract class EntitySoftDelete : Entity, ISoftDelete
    {
        public virtual bool IsDeleted { get; set; }
    }

}

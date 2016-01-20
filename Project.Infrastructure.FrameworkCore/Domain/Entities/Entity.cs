using System;


namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
    public interface  IEntity<TPrimaryKey>
    {
          TPrimaryKey PkId { get;  set; }
          int Version { get; set; }
    }

    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {   
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TPrimaryKey>);
        }

        private static bool IsTransient(Entity<TPrimaryKey> obj)
        {
            return obj != null &&
                   Equals(obj.PkId, default(TPrimaryKey));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TPrimaryKey> other)
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
            if (Equals(PkId, default(TPrimaryKey)))
                return base.GetHashCode();
            return PkId.GetHashCode();
        }

        public virtual TPrimaryKey PkId { get; set; }
        public virtual int Version { get; set; }
    }

    [Serializable]
    public abstract class Entity : Entity<int>
    {

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    //[Serializable]
    //public  class EntityWithTreeUi : ITree
    //{
    //    public string _parentId { get; set; }
    //}


    //[Serializable]
    //public abstract class EntitySoftDelete<T> : Entity<T>, ISoftDelete
    //{
    //    public virtual bool IsDeleted { get; set; }
    //}

   
}

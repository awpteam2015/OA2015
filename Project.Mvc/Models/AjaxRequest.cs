using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Model.PermissionManager;

namespace Project.Mvc.Models
{
    [Serializable]
    public class AjaxRequest<TRequestEntity> where TRequestEntity : IEntity<int>, new()
    {
        public AjaxRequest()
        {
            RequestEntity = new TRequestEntity();
        }
        public string Command { get; set; }

        public TRequestEntity RequestEntity { get; set; }
    }


    [Serializable]
    public class AjaxRequest<TRequestEntity, TPrimaryKey> where TRequestEntity : IEntity<TPrimaryKey>, new()
    {
        public AjaxRequest()
        {
            RequestEntity = new TRequestEntity();
        }
        public string Command { get; set; }

        public TRequestEntity RequestEntity { get; set; }
    }
}

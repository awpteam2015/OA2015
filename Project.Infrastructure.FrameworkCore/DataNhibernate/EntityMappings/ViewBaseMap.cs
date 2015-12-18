using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings
{
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

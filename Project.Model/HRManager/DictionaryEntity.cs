

/***************************************************************************
*       功能：     SMDictionary实体类
*       作者：     ROY
*       日期：     2016-01-10
*       描述：     
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi;
using Project.Model.Enum;

namespace Project.Model.HRManager
{
    public class DictionaryEntity: Entity, ITree
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String KeyCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ParentKeyCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String KeyName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String KeyValue{get; set;}
        #endregion


        #region 新增属性
        public virtual IList<DictionaryEntity> children { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public virtual string id
        {
            get { return KeyCode; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String text
        {
            get { return KeyName; }
        }



        private string parentId;
        public virtual System.String _parentId
        {
            get { return (ParentKeyCode == "" || ParentKeyCode == "0" || parentId == TreeInvalidCodeEnum.Invalid.ToString()) ? null : ParentKeyCode; }
            set { this.parentId = value; }
        }
        #endregion
    }
}

    
 


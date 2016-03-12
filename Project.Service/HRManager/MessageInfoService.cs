
/***************************************************************************
*       功能：     SysMessageInfo业务处理层
*       作者：     Roy
*       日期：     2016-02-21
*       描述：     站内短信
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class MessageInfoService
    {

        #region 构造函数
        private readonly MessageInfoRepository _messageInfoRepository;
        private static readonly MessageInfoService Instance = new MessageInfoService();

        public MessageInfoService()
        {
            this._messageInfoRepository = new MessageInfoRepository();
        }

        public static MessageInfoService GetInstance()
        {
            return Instance;
        }
        #endregion


        #region 基础方法 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public System.Int32 Add(MessageInfoEntity entity)
        {
            return _messageInfoRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _messageInfoRepository.GetById(pkId);
                _messageInfoRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(MessageInfoEntity entity)
        {
            try
            {
                _messageInfoRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(MessageInfoEntity entity)
        {
            try
            {
                _messageInfoRepository.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public MessageInfoEntity GetModelByPk(System.Int32 pkId)
        {
            return _messageInfoRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【站内短信】和总【站内短信】数</returns>
        public System.Tuple<IList<MessageInfoEntity>, int> Search(MessageInfoEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<MessageInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.MesTitle))
            //  expr = expr.And(p => p.MesTitle == where.MesTitle);
            // if (!string.IsNullOrEmpty(where.MesContent))
            //  expr = expr.And(p => p.MesContent == where.MesContent);
            // if (!string.IsNullOrEmpty(where.ReceiveUserCode))
            //  expr = expr.And(p => p.ReceiveUserCode == where.ReceiveUserCode);
            // if (!string.IsNullOrEmpty(where.IsAll))
            //  expr = expr.And(p => p.IsAll == where.IsAll);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            if (where.CreationTime.HasValue && where.CreationTime.Value.Year > 1)
                expr = expr.And(p => p.CreationTime >= where.CreationTime);
            if (where.CreationTimeEnd.HasValue && where.CreationTimeEnd.Value.Year > 1)
                expr = expr.And(p => p.CreationTime <= where.CreationTimeEnd.Value.AddDays(1));

            if (where.InfoType.HasValue && where.InfoType.Value >= 1)
                expr = expr.And(p => p.InfoType == where.InfoType);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            //if (!string.IsNullOrEmpty(where.CurUserCode))
            //    expr = expr.And(p => p.ReadUser.Contains(where.CurUserCode + ","));
            #endregion
            var list = _messageInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            list.ForEach(item => item.IsRead = string.IsNullOrWhiteSpace(item.ReadUser) ? false : item.ReadUser.Contains(where.CurUserCode + ","));
            var count = _messageInfoRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<MessageInfoEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<MessageInfoEntity> GetList(MessageInfoEntity where)
        {
            var expr = PredicateBuilder.True<MessageInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.MesTitle))
            //  expr = expr.And(p => p.MesTitle == where.MesTitle);
            // if (!string.IsNullOrEmpty(where.MesContent))
            //  expr = expr.And(p => p.MesContent == where.MesContent);
            // if (!string.IsNullOrEmpty(where.ReceiveUserCode))
            //  expr = expr.And(p => p.ReceiveUserCode == where.ReceiveUserCode);
            // if (!string.IsNullOrEmpty(where.IsAll))
            //  expr = expr.And(p => p.IsAll == where.IsAll);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            #endregion
            var list = _messageInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





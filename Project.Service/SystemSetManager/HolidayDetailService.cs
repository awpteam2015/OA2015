
/***************************************************************************
*       功能：     SysHolidayDetail业务处理层
*       作者：     Roy
*       日期：     2016-01-20
*       描述：     节假日管理
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SystemSetManager;
using Project.Repository.SystemSetManager;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using System;

namespace Project.Service.SystemSetManager
{
    public class HolidayDetailService
    {

        #region 构造函数
        private readonly HolidayDetailRepository _holidayDetailRepository;
        private static readonly HolidayDetailService Instance = new HolidayDetailService();

        public HolidayDetailService()
        {
            this._holidayDetailRepository = new HolidayDetailRepository();
        }

        public static HolidayDetailService GetInstance()
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
        public Tuple<bool, string> Add(HolidayDetailEntity entity)
        {

            //using (var tx = NhTransactionHelper.BeginTransaction())
            //{
            try
            {
                List<HolidayDetailEntity> entityList = new List<HolidayDetailEntity>();
                var difDays = (entity.HolidayDateEnd.Value - entity.HolidayDate.Value).Days + 1;
                var tempdate = entity.HolidayDate.Value;
                if (entity.HolidayDateType == 1)
                {
                    for (int i = 0; i < difDays; i++)
                    {
                        entity.HolidayDate = tempdate.AddDays(i);
                        entityList.Add((HolidayDetailEntity)entity.Clone());
                    }
                }
                else
                {
                    int[] gxDays = { (int)DayOfWeek.Sunday, (int)DayOfWeek.Saturday };
                    for (int i = 0; i < difDays; i++)
                    {
                        if (gxDays.Contains((int)tempdate.AddDays(i).DayOfWeek))
                        {
                            entity.HolidayDate = tempdate.AddDays(i);

                            entityList.Add((HolidayDetailEntity)entity.Clone());
                        }
                    }
                }

                _holidayDetailRepository.Merge(entityList);
                //  tx.Commit();
                return new Tuple<bool, string>(true, ""); ;
            }
            catch (Exception e)
            {
                //    tx.Rollback();
                throw;
            }
            //}
            //return _holidayDetailRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _holidayDetailRepository.GetById(pkId);
                _holidayDetailRepository.Delete(entity);
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
        public bool Delete(HolidayDetailEntity entity)
        {
            try
            {
                _holidayDetailRepository.Delete(entity);
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
        public bool Update(HolidayDetailEntity entity)
        {
            try
            {
                _holidayDetailRepository.Update(entity);
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
        public HolidayDetailEntity GetModelByPk(System.Int32 pkId)
        {
            return _holidayDetailRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【节假日管理】和总【节假日管理】数</returns>
        public System.Tuple<IList<HolidayDetailEntity>, int> Search(HolidayDetailEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<HolidayDetailEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);

            if (where.HolidayDateType >= 0)
                expr = expr.And(p => p.HolidayDateType == where.HolidayDateType);
            if (!string.IsNullOrEmpty(where.HolidayName))
                expr = expr.And(p => p.HolidayName.Contains(where.HolidayName));
            if (where.HolidayDate.HasValue && where.HolidayDate.Value.Year > 1)
                expr = expr.And(p => p.HolidayDate >= where.HolidayDate);
            if (where.HolidayDateEnd.HasValue && where.HolidayDateEnd.Value.Year > 1)
                expr = expr.And(p => p.HolidayDateEnd < where.HolidayDate);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            #endregion
            var list = _holidayDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _holidayDetailRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<HolidayDetailEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<HolidayDetailEntity> GetList(HolidayDetailEntity where)
        {
            var expr = PredicateBuilder.True<HolidayDetailEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);

            if (!string.IsNullOrEmpty(where.HolidayName))
                expr = expr.And(p => p.HolidayName == where.HolidayName);
            if (where.HolidayDate.HasValue && where.HolidayDate.Value.Year > 1)
                expr = expr.And(p => p.HolidayDate >= where.HolidayDate);
            if (where.HolidayDateEnd.HasValue && where.HolidayDateEnd.Value.Year > 1)
                expr = expr.And(p => p.HolidayDate < where.HolidayDateEnd);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            #endregion
            var list = _holidayDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





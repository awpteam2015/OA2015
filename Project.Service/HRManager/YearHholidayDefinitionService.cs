﻿
/***************************************************************************
*       功能：     HRYearHholidayDefinition业务处理层
*       作者：     Roy
*       日期：     2016-01-22
*       描述：     年休存休月定义
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;
using Project.Service.HRManager.Validate;
using System;

namespace Project.Service.HRManager
{
    public class YearHholidayDefinitionService
    {

        #region 构造函数
        private readonly YearHolidayDefinitionRepository _yearHholidayDefinitionRepository;
        private static readonly YearHholidayDefinitionService Instance = new YearHholidayDefinitionService();

        public YearHholidayDefinitionService()
        {
            this._yearHholidayDefinitionRepository = new YearHolidayDefinitionRepository();
        }

        public static YearHholidayDefinitionService GetInstance()
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
        public Tuple<bool, string> Add(YearHolidayDefinitionEntity entity)
        {
            //判断年份是否已经存在
            var validateResult = YearHolidayDefinitionValidate.GetInstance().IsHasSameYearsNum(entity.YearsNum.Value);
            if (!validateResult.Item1)
            {
                return validateResult;
            }
            var addResult = _yearHholidayDefinitionRepository.Save(entity);
            if (addResult > 0)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "");
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _yearHholidayDefinitionRepository.GetById(pkId);
                _yearHholidayDefinitionRepository.Delete(entity);
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
        public bool Delete(YearHolidayDefinitionEntity entity)
        {
            try
            {
                _yearHholidayDefinitionRepository.Delete(entity);
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
        public Tuple<bool, string> Update(YearHolidayDefinitionEntity entity)
        {
            try
            {
                //判断年份是否已经存在
                var validateResult = YearHolidayDefinitionValidate.GetInstance().IsHasSameYearsNum(entity.YearsNum.Value);
                if (!validateResult.Item1)
                {
                    return validateResult;
                }
                _yearHholidayDefinitionRepository.Update(entity);
                return new Tuple<bool, string>(true, "");
            }
            catch
            {
                return new Tuple<bool, string>(false, "");
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public YearHolidayDefinitionEntity GetModelByPk(System.Int32 pkId)
        {
            return _yearHholidayDefinitionRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【年休存休月定义】和总【年休存休月定义】数</returns>
        public System.Tuple<IList<YearHolidayDefinitionEntity>, int> Search(YearHolidayDefinitionEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<YearHolidayDefinitionEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.YearsNum > 0)
                expr = expr.And(p => p.YearsNum == where.YearsNum);
            // if (!string.IsNullOrEmpty(where.BeginMonth))
            //  expr = expr.And(p => p.BeginMonth == where.BeginMonth);
            // if (!string.IsNullOrEmpty(where.EndMonth))
            //  expr = expr.And(p => p.EndMonth == where.EndMonth);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            #endregion
            var list = _yearHholidayDefinitionRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _yearHholidayDefinitionRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<YearHolidayDefinitionEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<YearHolidayDefinitionEntity> GetList(YearHolidayDefinitionEntity where)
        {
            var expr = PredicateBuilder.True<YearHolidayDefinitionEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.YearsNum))
            //  expr = expr.And(p => p.YearsNum == where.YearsNum);
            // if (!string.IsNullOrEmpty(where.BeginMonth))
            //  expr = expr.And(p => p.BeginMonth == where.BeginMonth);
            // if (!string.IsNullOrEmpty(where.EndMonth))
            //  expr = expr.And(p => p.EndMonth == where.EndMonth);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            #endregion
            var list = _yearHholidayDefinitionRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





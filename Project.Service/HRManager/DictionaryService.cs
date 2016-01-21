
/***************************************************************************
*       功能：     SMDictionary业务处理层
*       作者：     ROY
*       日期：     2016-01-10
*       描述：     
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class DictionaryService
    {

        #region 构造函数
        private readonly DictionaryRepository _dictionaryRepository;
        private static readonly DictionaryService Instance = new DictionaryService();

        public DictionaryService()
        {
            this._dictionaryRepository = new DictionaryRepository();
        }

        public static DictionaryService GetInstance()
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
        public System.Int32 Add(DictionaryEntity entity)
        {
            return _dictionaryRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _dictionaryRepository.GetById(pkId);
                _dictionaryRepository.Delete(entity);
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
        public bool Delete(DictionaryEntity entity)
        {
            try
            {
                _dictionaryRepository.Delete(entity);
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
        public bool Update(DictionaryEntity entity)
        {
            try
            {
                _dictionaryRepository.Update(entity);
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
        public DictionaryEntity GetModelByPk(System.Int32 pkId)
        {
            return _dictionaryRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<DictionaryEntity>, int> Search(DictionaryEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<DictionaryEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.KeyCode))
            //  expr = expr.And(p => p.KeyCode == where.KeyCode);
            // if (!string.IsNullOrEmpty(where.ParentKeyCode))
            //  expr = expr.And(p => p.ParentKeyCode == where.ParentKeyCode);
            // if (!string.IsNullOrEmpty(where.KeyName))
            //  expr = expr.And(p => p.KeyName == where.KeyName);
            // if (!string.IsNullOrEmpty(where.KeyValue))
            //  expr = expr.And(p => p.KeyValue == where.KeyValue);
            #endregion
            var list = _dictionaryRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _dictionaryRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<DictionaryEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<DictionaryEntity> GetList(DictionaryEntity where)
        {
            var expr = PredicateBuilder.True<DictionaryEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.KeyCode))
            //  expr = expr.And(p => p.KeyCode == where.KeyCode);
            if (!string.IsNullOrEmpty(where.ParentKeyCode))
                expr = expr.And(p => p.ParentKeyCode == where.ParentKeyCode);
            // if (!string.IsNullOrEmpty(where.KeyName))
            //  expr = expr.And(p => p.KeyName == where.KeyName);
            // if (!string.IsNullOrEmpty(where.KeyValue))
            //  expr = expr.And(p => p.KeyValue == where.KeyValue);
            #endregion
            var list = _dictionaryRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        public IList<DictionaryEntity> GetTreeList(DictionaryEntity entity, bool isShowTop = false)
        {
            var listAll = this.GetList(entity);

            var list = new List<DictionaryEntity>();
            if (isShowTop)
            {
                list.Add(new DictionaryEntity() { KeyCode = "0", KeyName = "顶级节点" });
            }
            else
            {
                list.AddRange(listAll.Where(p => p.ParentKeyCode == "0"));
            }

            list.ForEach(p =>
            {
                GetChildList(listAll, p);
            }
            );

            return list;
        }
        private void GetChildList(IList<DictionaryEntity> allList, DictionaryEntity parentDictionaryEntity)
        {

            var childList = allList.Where(p => p.ParentKeyCode == parentDictionaryEntity.KeyCode).ToList();
            if (childList.Any())
            {
                parentDictionaryEntity.children = childList;
                childList.ForEach(p =>
                {
                    GetChildList(allList, p);
                });
            }

        }

        #endregion
    }
}





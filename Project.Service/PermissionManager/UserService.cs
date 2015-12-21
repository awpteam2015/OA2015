using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class UserInfoService
    {
        #region
        private static readonly UserInfoService Instance = new UserInfoService();
        private readonly UserInfoRepository _userInfoRepository;

        private UserInfoService()
        {
            _userInfoRepository = new UserInfoRepository();
        }

        public static UserInfoService GetInstance()
        {
            return Instance;
        }

        public void Test()
        {
            var t = _userInfoRepository.GetById(1);
        }
        #endregion


        public UserInfoEntity GetModel(int pkId)
        {
            return _userInfoRepository.GetById(pkId);
        }

        public int Add(UserInfoEntity entity)
        {
            return _userInfoRepository.Save(entity);
        }

        public bool Update(UserInfoEntity entity)
        {
            try
            {
                _userInfoRepository.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(UserInfoEntity entity)
        {
            try
            {
                _userInfoRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public System.Tuple<IList<UserInfoEntity>, int> Search(UserInfoEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();
            #region
            if (!string.IsNullOrWhiteSpace(where.UserCode))
                expr = expr.And(p => p.UserCode == where.UserCode);
            #endregion

            var list = _userInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(skipResults + maxResults).ToList();
            var count = _userInfoRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<UserInfoEntity>, int>(list, count);
        }


    }
}

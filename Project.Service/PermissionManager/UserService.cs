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
    public class UserService
    {
        #region
        private static readonly UserService Instance = new UserService();
        private readonly UserInfoRepository _userInfoRepository;

        private UserService()
        {
            _userInfoRepository = new UserInfoRepository();
        }

        public static UserService GetInstance()
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
        /// <param name="temp"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public System.Tuple<IList<UserInfoEntity>, int> Search(UserInfoEntity temp, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();
            #region
            if (!string.IsNullOrWhiteSpace(temp.UserCode))
                expr = expr.And(p => p.UserCode == temp.UserCode);
            #endregion

            var list = _userInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(skipResults + maxResults).ToList();
            var count = _userInfoRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<UserInfoEntity>, int>(list, count);
        }


    }
}

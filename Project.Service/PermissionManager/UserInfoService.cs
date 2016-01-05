using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NHibernate.Mapping;
using NHibernate.Util;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;
using Project.Service.PermissionManager.DTO;
using Project.Service.PermissionManager.Validate;

namespace Project.Service.PermissionManager
{
    public class UserInfoService
    {
        #region
        private static readonly UserInfoService Instance = new UserInfoService();
        private readonly UserInfoRepository _userInfoRepository;
        private readonly UserDepartmentRepository _userDepartmentRepository;
        private readonly UserRoleRepository _userRoleRepository;
        private readonly FunctionDetailRepository _functionDetailRepository;
        private readonly UserFunctionDetailRepository _userFunctionDetailRepository;

        private UserInfoService()
        {
            _userInfoRepository = new UserInfoRepository();
            _userDepartmentRepository = new UserDepartmentRepository();
            _userRoleRepository = new UserRoleRepository();
            this._functionDetailRepository = new FunctionDetailRepository();
            _userFunctionDetailRepository = new UserFunctionDetailRepository();
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


        public Tuple<bool, string, LoginUserInfoDTO> Login(string userCode, string password)
        {
            var userInfoEntity = this.GetList(new UserInfoEntity() { UserCode = userCode, Password = Encrypt.MD5Encrypt(password) }).FirstOrDefault();
            if (userInfoEntity != null)
            {
                userInfoEntity.UserFunctionDetailList_Checked = this.GetFunctionDetailList_Checked(userCode);

                var loginUserInfo = Mapper.Map<UserInfoEntity, LoginUserInfoDTO>(userInfoEntity);
                return new Tuple<bool, string, LoginUserInfoDTO>(true, "", loginUserInfo);
            }
            else
            {
                return new Tuple<bool, string, LoginUserInfoDTO>(false, "用户名或者密码错误！", null);
            }
        }


        /// <summary>
        /// 获取用户对应的菜单信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IList<MenuDTO> GetMenuDTOList(string userCode, IList<int> permissionCodeList)
        {
            var allPermissionFunction = PermissionService.GetInstance().GetAllPermissionFunction();

            var userPermissionFunction = PermissionService.GetInstance().GetAllPermissionFunction();
            if (!PermissionService.GetInstance().IsAdmin(userCode))
            {
                userPermissionFunction = userPermissionFunction.Where(p => permissionCodeList.Contains(p.PkId)).ToList();
            }
            var module = userPermissionFunction.Select(p => p.ModuleId).Distinct().ToList();
            var function = userPermissionFunction.Select(p => p.FunctionId).Distinct().ToList();
            var menuDtoList = new List<MenuDTO>();
            var moduleList = ModuleService.GetInstance().GetList(new ModuleEntity());
            moduleList.Where(p => module.Contains(p.PkId)).ForEach(p =>
            {
                var temp = new MenuDTO()
                {
                    Url = "",
                    Name = p.ModuleName
                };

                var list = p.FunctionEntityList.Where(x => function.Contains(x.PkId)).ToList();

                list.ForEach(
                    x =>
                    {
                        if (x.IsDisplayOnMenu == 1)
                        {
                            temp.MenuDTOList.Add(new MenuDTO()
                            {
                                Url = x.FunctionUrl,
                                Name = x.FunctionnName
                            });
                        }
                    }
                    );
                if (temp.MenuDTOList.Any())
                {
                    menuDtoList.Add(temp);
                }
            });
            return menuDtoList;
        }



        public UserInfoEntity GetModel(int pkId)
        {
            return _userInfoRepository.GetById(pkId);
        }

        public UserInfoEntity GetUserInfo(string userCode)
        {
            return _userInfoRepository.Query().FirstOrDefault(p => p.UserCode == userCode);
        }


        public Tuple<bool, string> Add(UserInfoEntity entity)
        {
            var validateResult = UserInfoValidate.GetInstance().IsHasSameUserCode(entity.UserCode);
            if (!validateResult.Item1)
            {
                return validateResult;
            }
            var addResult = _userInfoRepository.Save(entity);
            if (addResult > 0)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "");
            }
        }

        public Tuple<bool, string> Update(UserInfoEntity entity)
        {
            var validateResult = UserInfoValidate.GetInstance().IsHasSameUserCode(entity.UserCode, entity.PkId);
            if (!validateResult.Item1)
            {
                return validateResult;
            }

            var oldEntity = _userInfoRepository.GetById(entity.PkId);
            entity.CreationTime = oldEntity.CreationTime;
            entity.CreatorUserCode = oldEntity.CreatorUserCode;
            if (entity.Password != oldEntity.Password)
            {
                entity.Password = Encrypt.MD5Encrypt(entity.Password);//加密
            }

            var deleteList = oldEntity.UserDepartmentList.Where(
             p => entity.UserDepartmentList.All(x => x.DepartmentCode != p.DepartmentCode)).ToList();

            var deleteList2 = oldEntity.UserRoleList.Where(
                  p => entity.UserRoleList.All(x => x.RoleId != p.RoleId)).ToList();

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _userInfoRepository.Merge(entity);
                    deleteList.ForEach(p => { _userDepartmentRepository.Delete(p); });
                    deleteList2.ForEach(p => { _userRoleRepository.Delete(p); });
                    tx.Commit();
                    return new Tuple<bool, string>(true, "");
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public bool Delete(int pkId)
        {
            try
            {
                var entity = _userInfoRepository.GetById(pkId);
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
        public System.Tuple<IList<UserInfoEntity>, int> Search(UserInfoEntity entity, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.UserCode))
                expr = expr.And(p => p.UserCode == entity.UserCode);
            // if (!string.IsNullOrEmpty(entity.Password))
            //  expr = expr.And(p => p.Password == entity.Password);
            if (!string.IsNullOrEmpty(entity.UserName))
                expr = expr.And(p => p.UserName == entity.UserName);
            // if (!string.IsNullOrEmpty(entity.Email))
            //  expr = expr.And(p => p.Email == entity.Email);
            if (!string.IsNullOrEmpty(entity.Mobile))
                expr = expr.And(p => p.Mobile == entity.Mobile);
            // if (!string.IsNullOrEmpty(entity.Tel))
            //  expr = expr.And(p => p.Tel == entity.Tel);
            if (entity.IsActive != 0)
                expr = expr.And(p => p.IsActive == entity.IsActive);
            // if (!string.IsNullOrEmpty(entity.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == entity.CreatorUserCode);
            // if (!string.IsNullOrEmpty(entity.CreationTime))
            //  expr = expr.And(p => p.CreationTime == entity.CreationTime);
            // if (!string.IsNullOrEmpty(entity.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == entity.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(entity.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == entity.LastModificationTime);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            // if (!string.IsNullOrEmpty(entity.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == entity.IsDeleted);
            #endregion

            var list = _userInfoRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(skipResults + maxResults).ToList();
            var count = _userInfoRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<UserInfoEntity>, int>(list, count);
        }


        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<UserInfoEntity> GetList(UserInfoEntity entity)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.UserCode))
                expr = expr.And(p => p.UserCode == entity.UserCode);
            if (!string.IsNullOrEmpty(entity.Password))
                expr = expr.And(p => p.Password == entity.Password);
            // if (!string.IsNullOrEmpty(entity.UserName))
            //  expr = expr.And(p => p.UserName == entity.UserName);
            // if (!string.IsNullOrEmpty(entity.Email))
            //  expr = expr.And(p => p.Email == entity.Email);
            // if (!string.IsNullOrEmpty(entity.Mobile))
            //  expr = expr.And(p => p.Mobile == entity.Mobile);
            // if (!string.IsNullOrEmpty(entity.Tel))
            //  expr = expr.And(p => p.Tel == entity.Tel);
            // if (!string.IsNullOrEmpty(entity.IsActive))
            //  expr = expr.And(p => p.IsActive == entity.IsActive);
            // if (!string.IsNullOrEmpty(entity.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == entity.CreatorUserCode);
            // if (!string.IsNullOrEmpty(entity.CreationTime))
            //  expr = expr.And(p => p.CreationTime == entity.CreationTime);
            // if (!string.IsNullOrEmpty(entity.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == entity.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(entity.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == entity.LastModificationTime);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            // if (!string.IsNullOrEmpty(entity.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == entity.IsDeleted);
            #endregion
            var list = _userInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }


        public IList<UserFunctionDetailEntity> GetUserFunctionDetailList(UserFunctionDetailEntity entity)
        {
            var expr = PredicateBuilder.True<UserFunctionDetailEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.UserCode))
                expr = expr.And(p => p.UserCode == entity.UserCode);
            if (entity.FunctionId > 0)
                expr = expr.And(p => p.FunctionId == entity.FunctionId);
            if (entity.FunctionDetailId > 0)
                expr = expr.And(p => p.FunctionDetailId == entity.FunctionDetailId);
            #endregion
            var list = _userFunctionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }


        /// <summary>
        /// 获取当前用户所属详细功能
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<int> GetFunctionDetailList_Checked(string userCode)
        {
            var list = new List<int>();
            var userInfo = this.GetUserInfo(userCode);
            userInfo.UserRoleList.ToList().ForEach(p =>
            {
                var roleFunctionDetailList = RoleService.GetInstance().GetFunctionDetailList_Checked(p.RoleId);
                list.AddRange(roleFunctionDetailList);
            });

            list = list.Distinct().ToList();
            userInfo.UserFunctionDetailList.ToList().ForEach(p =>
            {
                if (p.State == 1)
                {
                    list.Add(p.FunctionDetailId);
                }
                else
                {
                    list.Remove(p.FunctionDetailId);
                }
            });

            return list.Distinct().ToList(); ;
        }


        /// <summary>
        /// 设置用户权限
        /// </summary>
        public bool SetRowFunction(string userCode, int functionPkId, int functionDetailPkId, bool isCheck)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var checkList = GetFunctionDetailList_Checked(userCode);

                    var functionDetailList = new List<FunctionDetailEntity>();
                    if (functionPkId > 0)
                    {
                        functionDetailList = FunctionService.GetInstance().GetFunctionDetailList(new FunctionDetailEntity() { FunctionId = functionPkId }).ToList();
                    }
                    else
                    {
                        functionDetailList.Add(_functionDetailRepository.GetById(functionDetailPkId));
                    }




                    if (isCheck == true)
                    {
                        var addList = functionDetailList.Where(p => checkList.All(x => x != p.PkId)).ToList();
                        addList.ForEach(p =>
                        {
                            //DeleteUserFunctionDetail(userCode, p.PkId, p.FunctionId);

                            var entiy =
                                GetUserFunctionDetailList(new UserFunctionDetailEntity()
                                {
                                    UserCode = userCode,
                                    FunctionDetailId = p.PkId,
                                    FunctionId = p.FunctionId
                                }).FirstOrDefault();
                            if (entiy == null)
                            {
                                var temp = new UserFunctionDetailEntity();
                                temp.FunctionDetailId = p.PkId;
                                temp.FunctionId = p.FunctionId;
                                temp.State = 1;
                                temp.UserCode = userCode;
                                _userFunctionDetailRepository.Save(temp);
                            }
                            else
                            {
                                entiy.State = 1;
                                _userFunctionDetailRepository.Update(entiy);
                            }


                        });
                    }
                    else
                    {
                        var userFunctionDetailList =
                            _userFunctionDetailRepository.Query().Where(p => p.UserCode == userCode).ToList();

                        var delList = functionDetailList.Where(p => userFunctionDetailList.Any(x => x.FunctionDetailId == p.PkId)).ToList();
                        delList.ForEach(p =>
                        {
                            var updateEntity = userFunctionDetailList.FirstOrDefault(x => x.FunctionDetailId == p.PkId);
                            updateEntity.State = -1;
                            _userFunctionDetailRepository.Update(updateEntity);
                        });

                        var addList = functionDetailList.Where(p => userFunctionDetailList.All(x => x.FunctionDetailId != p.PkId)).ToList();
                        addList.ForEach(p =>
                        {
                            var entiy =
                               GetUserFunctionDetailList(new UserFunctionDetailEntity()
                               {
                                   UserCode = userCode,
                                   FunctionDetailId = p.PkId,
                                   FunctionId = p.FunctionId
                               }).FirstOrDefault();
                            if (entiy == null)
                            {
                                var temp = new UserFunctionDetailEntity();
                                temp.FunctionDetailId = p.PkId;
                                temp.FunctionId = p.FunctionId;
                                temp.State = 1;
                                temp.UserCode = userCode;
                                _userFunctionDetailRepository.Save(temp);
                            }
                            else
                            {
                                entiy.State = 1;
                                _userFunctionDetailRepository.Update(entiy);
                            }
                        });

                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }






    }


}

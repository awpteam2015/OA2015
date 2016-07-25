using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager.Validate
{
    public class DictionaryValidate
    {
        private static readonly DictionaryValidate Instance = new DictionaryValidate();

        private DictionaryValidate()
        {
        }

        public static DictionaryValidate GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// 是否存在相同的编号
        /// </summary>
        /// <param name="newUserCode"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameKeyCode(string newKeyCode, int pkId = 0)
        {
            var list = DictionaryService.GetInstance().GetList(new DictionaryEntity() { KeyCode = newKeyCode });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的编号！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的编号！");
            }

            return new Tuple<bool, string>(true, "");
        }
    }
}

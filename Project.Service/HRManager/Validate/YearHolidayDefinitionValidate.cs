using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;


namespace Project.Service.HRManager.Validate
{
    public class YearHolidayDefinitionValidate
    {
        #region
        private static readonly YearHolidayDefinitionValidate Instance = new YearHolidayDefinitionValidate();
        private readonly YearHolidayDefinitionRepository _yearHholidayDefinitionRepository;

        private YearHolidayDefinitionValidate()
        {
            _yearHholidayDefinitionRepository = new YearHolidayDefinitionRepository();
        }

        public static YearHolidayDefinitionValidate GetInstance()
        {
            return Instance;
        }

        #endregion

        /// <summary>
        /// 是否存在年休定义
        /// </summary>
        /// <param name="newYearsNum"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameYearsNum(int newYearsNum, int pkId = 0)
        {
            var list = YearHholidayDefinitionService.GetInstance().GetList(new YearHolidayDefinitionEntity() { YearsNum = newYearsNum });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在相同的年份！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在相同的年份！");
            }

            return new Tuple<bool, string>(true, "存在相同的年份！");
        }
    }
}

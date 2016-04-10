using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.PermissionManager.DTO
{
    public class MenuDTO
    {
        public MenuDTO()
        {
            MenuDTOList = new List<MenuDTO>();
        }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? RankId { get; set; }

        public IList<MenuDTO> MenuDTOList { get; set; }
    }
}

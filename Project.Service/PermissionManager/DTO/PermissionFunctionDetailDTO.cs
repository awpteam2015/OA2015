namespace Project.Service.PermissionManager.DTO
{
    public class PermissionFunctionDetailDTO
    {
        public  int PkId { get; set; }

        public  System.String FunctionDetailName { get; set; }
        /// <summary>
        /// 功能代号对应页面需要控制的按钮Id
        /// </summary>
        public  System.String FunctionDetailCode { get; set; }


        public System.Int32 ModuleId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public  System.Int32 FunctionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  System.String Area { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  System.String Controller { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  System.String Action { get; set; }
    }
}

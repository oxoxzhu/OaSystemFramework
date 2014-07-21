using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.Model;

namespace HeiMa8.OA.IBLL
{
    public partial interface IActionInfoBLL:IBaseBLL<ActionInfo>
    {
        bool SetRole(int userId, List<int> roleIds);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBusinessOrder.MenDian
{
    public interface IOPMenDian
    {
        /// <summary>
        /// 获取门店列表信息
        /// </summary>
        /// <returns></returns>
        List<DataModel.MenDian> GetMenDianList();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel.BaseInfo
{
    /// <summary>
    /// 颜色组 品牌商维护（在用）
    /// </summary>
    public class ColorGroups : QSmartEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int BrandId { get; set;}
        [StringMaxLength(50, VarCharType.nvarchar)]
        public string Name { get; set; }
    }
}
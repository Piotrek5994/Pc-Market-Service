﻿using Pc_Market_Service.Model.PcMarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Service.IService
{
    public interface IPcMarketService
    {
        List<DocumentDto> MapQueryResult();
    }
}
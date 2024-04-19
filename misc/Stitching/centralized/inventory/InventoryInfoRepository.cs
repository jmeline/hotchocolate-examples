using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Inventory
{
    public class InventoryInfoRepository
    {
        private readonly Dictionary<int, InventoryInfo> _infos;

        public InventoryInfoRepository()
        {
            _infos = new InventoryInfo[]
            {
                new InventoryInfo(1, true, new DateOnly(2024, 04, 05)),
                new InventoryInfo(2, false, new DateOnly(2022, 03, 06)),
                new InventoryInfo(3, true, new DateOnly(2025, 02, 01))
            }.ToDictionary(t => t.Upc);
        }

        public InventoryInfo GetInventoryInfo(int upc) => _infos[upc];
    }
}
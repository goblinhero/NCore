using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Strategies;

namespace NCore.Demo.Utilities
{
    public class OrderLineIndexHandler
    {
        public void AdjustIndexes(OrderLine changingLine, int? newIndex, IList<OrderLine> otherLines)
        {
            new IStrategy<IList<OrderLine>, int>[]
            {
                //An index was set - other lines will be adjusted to take this into account
                new RelayStrategy<IList<OrderLine>, int>(ol =>
                {
                    var i = Math.Min(newIndex.Value, ol.Max(l => l.Index) + 1);
                    ol.Where(l => l.Index >= i).ForEach(l => l.Index++);
                    return i;
                }, ol => ol.Any() && newIndex.HasValue),
                //No index was set - the line will be placed last
                new RelayStrategy<IList<OrderLine>, int>(ol => ol.Max(l => l.Index) + 1, ol => ol.Any()),
                //No existing lines - index is set at 0
                new RelayStrategy<IList<OrderLine>, int>(ol => 0)
            }.ExecuteFirstOrDefault(otherLines);
        }
    }
}
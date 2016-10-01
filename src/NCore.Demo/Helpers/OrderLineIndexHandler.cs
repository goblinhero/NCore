using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Strategies;

namespace NCore.Demo.Helpers
{
    public class OrderLineIndexHandler
    {
        public void AdjustIndexes(OrderLine changingLine, int? newIndex, IList<OrderLine> otherLines)
        {
            new IStrategy<IList<OrderLine>, int>[]
            {
                new RelayStrategy<IList<OrderLine>, int>(ol =>
                {
                    var i = Math.Min(newIndex.Value, ol.Max(l => l.Index) + 1);
                    ol.Where(l => l.Index >= i).ForEach(l => l.Index++);
                    return i;
                }, ol => ol.Any() && newIndex.HasValue),
                new RelayStrategy<IList<OrderLine>, int>(ol => ol.Max(l => l.Index) + 1, ol => ol.Any()),
                new RelayStrategy<IList<OrderLine>, int>(ol => 0)
            }.ExecuteFirstOrDefault(otherLines);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Interfaces
{
    public interface IFilterable
    {
        List<IListItem> Filter(string filterCriteria);
    }
}

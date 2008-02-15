using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail.Parse
{
    public interface UpdatingObject
    {
        bool NeedsUpdate();
        bool Done();
    }
}

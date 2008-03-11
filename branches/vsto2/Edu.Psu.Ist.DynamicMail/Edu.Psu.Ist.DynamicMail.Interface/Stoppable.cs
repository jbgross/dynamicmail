using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    /// <summary>
    /// Generic interface for stoppable items;
    /// should be implemented by any component
    /// that uses a UI component and can be stopped
    /// </summary>
    public interface Stoppable
    {
        void Stop();
    }
}

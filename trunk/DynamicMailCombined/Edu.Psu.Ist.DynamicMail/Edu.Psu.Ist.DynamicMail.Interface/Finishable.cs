using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    /// <summary>
    /// Callback interface to indicate that a 
    /// Dialogue box is complete
    /// </summary>
    public interface Finishable
    {
        /// <summary>
        /// Call back to the calling code to indicate
        /// that this Dialogue box is finished
        /// </summary>
        void Finish();

        /// <summary>
        /// Call back that the action was cancelled
        /// </summary>
        void Cancel();

    }
}

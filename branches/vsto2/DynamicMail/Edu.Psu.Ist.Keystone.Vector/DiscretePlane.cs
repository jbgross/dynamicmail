using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    /// <summary>
    /// A plane with a single, discrete dimension.
    /// Two such planes could be used to do a 2-D clustering,
    /// but there is an explicit separate Dimension for 
    /// Cartesian 2-D and n-D planes
    /// </summary>
    public abstract class DiscretePlane : PlaneType
    {

    }
}

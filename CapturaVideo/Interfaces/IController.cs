using MultiCam.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCam
{
    /// <summary>
    /// Global application Controller interface
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Start controller application, initialize and show view
        /// </summary>
        /// <returns>View application</returns>
        System.Windows.Forms.Form Run();
    }
}

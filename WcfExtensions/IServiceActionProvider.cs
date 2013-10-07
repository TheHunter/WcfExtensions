using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServiceActionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        Action IncomingAction { get; }

        /// <summary>
        /// 
        /// </summary>
        Action OutgoingAction { get; }
        
    }
}

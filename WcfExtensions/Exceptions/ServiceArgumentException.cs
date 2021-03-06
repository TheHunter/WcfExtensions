﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfExtensions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceArgumentException
        : WcfServiceException
    {
        private readonly string parameter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ServiceArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameter"></param>
        public ServiceArgumentException(string message, string parameter)
            : base(message)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServiceArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameter"></param>
        /// <param name="innerException"></param>
        public ServiceArgumentException(string message, string parameter, Exception innerException)
            : base(message, innerException)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Parameter
        {
            get { return parameter; }
        }
    }
}

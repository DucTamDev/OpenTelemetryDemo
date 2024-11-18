﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Interceptors
{
    public interface IInterceptionProxy : IInterceptor
    {
        IInterceptor Interceptor { get; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Interceptors
{
    public interface ITypeInterceptor : IInterceptor
    {
        Type Intercept(Type typeToIntercept, Type interceptionType);

        bool CanIntercept(Type typeToIntercept);
    }
}

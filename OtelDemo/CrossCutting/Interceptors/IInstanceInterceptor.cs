using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Interceptors
{
    public interface IInstanceInterceptor :IInterceptor
    {
        object Intercept(object instance, Type typeToIntercept, IInterceptionHandler handler);

        bool CanIntercept(object instance);
    }
}

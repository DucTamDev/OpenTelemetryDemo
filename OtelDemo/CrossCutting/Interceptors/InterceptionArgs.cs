using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Interceptors
{
    [Serializable]
    public sealed class InterceptionEventArgs : EventArgs
    {
        private object? result;

        public InterceptionEventArgs(object instance, MethodInfo method, params object[] arguments)
        {
            this.Instance = instance;
            this.Method = method;
            this.Arguments = arguments;
        }

        public void Proceed()
        {
            this.Result = this.Method.Invoke(this.Instance, this.Arguments);
        }

        public object Instance
        {
            get;
            private set;
        }

        public MethodInfo Method
        {
            get;
            private set;
        }

        public object[] Arguments
        {
            get;
            private set;
        }

        public bool Handled
        {
            get;
            set;
        }

        public object? Result
        {
            get
            {
                return (this.result);
            }
            set
            {
                this.result = value;
                this.Handled = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(validOn: AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SecureSingletonAttribute : Attribute
    {

    }


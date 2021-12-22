using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace ReSharperAbp.Util
{
    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value: null => halt")]
        public static T NotNull<T>(T value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            return value;
        }
    }
}

using System;
using JetBrains.ReSharper.Psi;
using JetBrains.RiderTutorials.Utils;

namespace ReSharperAbp.Exceptions
{
    public class MultipleConstructorException : Exception
    {
        public ITypeElement Element { get; }

        public MultipleConstructorException(ITypeElement element) : base($"`{element.GetFullClrName()}` has more the one constructor")
        {
            Element = element;
        }
    }
}
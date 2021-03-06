using System;

namespace TypeConverter.Tests.Stubs
{
    public class Operators : IOperators, IGenericOperators<string>
    {
        public static implicit operator string(Operators o)
        {
            return "Operators";
        }

        public static implicit operator int(Operators o)
        {
            return 2;
        }

        public static explicit operator decimal?(Operators o)
        {
            return 3.456m;
        }

        public static explicit operator StringSplitOptions(Operators o)
        {
            return StringSplitOptions.RemoveEmptyEntries;
        }

        public static explicit operator Operators2(Operators o)
        {
            return new Operators2();
        }

        public string GenericProperty
        {
            get
            {
                return "GenericProperty";
            }
        }
    }

    public interface IOperators
    {
    }

    public interface IGenericOperators<out T>
    {
        T GenericProperty { get; }
    }
}
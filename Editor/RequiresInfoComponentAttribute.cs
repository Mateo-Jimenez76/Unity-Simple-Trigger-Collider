using System;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class RequiresInfoComponentAttribute : Attribute
{
    public Type RequiredComponentType { get; private set; }

    public RequiresInfoComponentAttribute(Type requiredComponentType)
    {
        RequiredComponentType = requiredComponentType;
    }
}
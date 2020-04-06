using System;


/// <summary>
/// We have to set the path where this object is loaded from the resources folder,
/// if the object doesn't include this attribute an invalid operation exception is gonna be raised.
/// </summary>
[AttributeUsage(validOn: AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class SingletonConfiguration : Attribute
{
    public string ResourcesPath { get; private set; }

    public SingletonConfiguration(string resourcesPath)
    {
        ResourcesPath = resourcesPath;
    }
}


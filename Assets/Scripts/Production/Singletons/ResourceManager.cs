using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[SecureSingleton]
public class ResourceManager : MonoSingleton<ResourceManager>
{
  public string GetJsonData()
    {
        
        return "This is my Json data";
    }
}

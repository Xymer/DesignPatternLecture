using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static FileManager Instance { get; } = new FileManager();

    private FileManager() { }

    public string GetFileName()
    {
       return "FileName";
    }
}

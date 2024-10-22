using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoldTools
{
    
    [MenuItem("Tools/Add 10 Gold")]
    public static void Add10GoldFromMenu()
    {
        CampManager.Instance.AddMoney(10);
    }
}

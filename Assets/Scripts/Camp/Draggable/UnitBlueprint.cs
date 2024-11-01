using System;
using UnityEngine;

[Serializable]
public class UnitBlueprint
{
    public UnitConfig Config;
    public int Level;
    public bool AgressiveMode;
    public void LevelUp()
    {
        Level++;
    }
}

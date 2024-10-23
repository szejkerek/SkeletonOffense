using System;
using UnityEngine;

[Serializable]
public class UnitBlueprint 
{
    public UnitConfig config;
    public int level = 1;
    public bool agressiveMode = false;

    public UnitBlueprint(UnitConfig config)
    { 
        this.config = config;
        level = 1;
    }

    public void SpawnUnit()
    {
        Debug.Log("Unit" + config.name + "spawned");
    }

    public void LevelUp()
    {
        level++;
    }
}

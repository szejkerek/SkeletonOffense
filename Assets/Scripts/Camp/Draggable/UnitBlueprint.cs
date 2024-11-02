using System;
using UnityEngine;

[Serializable]
public class UnitBlueprint
{
    public UnitConfig Config;
    public int Tier;
    public bool AgressiveMode;
    public void TierUp()
    {
        Tier++;
    }
}

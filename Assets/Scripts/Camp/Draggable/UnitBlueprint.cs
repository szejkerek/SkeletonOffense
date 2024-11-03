using System;
using UnityEngine;

[Serializable]
public class UnitBlueprint
{
    public UnitConfig Config;
    public int Tier;
    public bool Agressive;
    public void TierUp()
    {
        Tier++;
    }
}

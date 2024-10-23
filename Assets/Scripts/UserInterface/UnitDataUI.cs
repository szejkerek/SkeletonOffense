using UnityEngine;

public class UnitDataUI
{
    public UnitConfig config;
    public int unlockRound;

    public UnitDataUI(UnitConfig config)
    {
        this.config = config;
        this.unlockRound = config.unlockRound;
    }

    void DecreaseUnlockRound()
    {
        if(unlockRound > 1)this.unlockRound--; 
    }

}

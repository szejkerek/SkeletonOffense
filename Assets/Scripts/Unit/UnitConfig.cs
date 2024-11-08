using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Scriptable Objects/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [Header("Walk")]
    public float height = 1.5f;
    public float walkSpeed = 5f;
    [Header("Health")]
    public int health = 100;

    [Header("Price")]
    public int price = 5;
    [Header("UnlockTime")]
    public int unlockStage = 1;
    public int unlockRound = 1; // Modify to percentage, because stages will have diferent num of rounds

    [Header("UnitModels")]
    public GameObject UnitModelTier1;
    public Sprite IconTier1;
    public GameObject UnitModelTier2;
    public Sprite IconTier2;
    public GameObject UnitModelTier3;
    public Sprite IconTier3;
    public int BulkSpawnCount = 1; //Count of units spawned on stage at once (later swap to LevelsToAddNextUnit and calculate later)

}

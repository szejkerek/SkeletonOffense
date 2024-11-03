using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<StageManager> stagesOrder = new();
    public StageManager currentStage {get; private set;}
    
    void Awake()
    {
        if(stagesOrder.Count == 0)
        {
            Debug.LogError("Assign at least one stage manager");
            return;
        }

        currentStage = stagesOrder.FirstOrDefault();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public StageManager currentStage { get; private set; }
    
    [SerializeField] CampManager campManager;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] List<StageManager> stagesOrder = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;

        if (stagesOrder.Count == 0)
        {
            Debug.LogError("Assign at least one stage manager");
            return;
        }

        currentStage = stagesOrder.FirstOrDefault();
        
        ComeToCamp();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ComeToCamp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ComeToStage();
        }
    }

    void ComeToStage()
    {
        cameraManager.ApplyConfig(currentStage.CameraConfig);
    }

    void ComeToCamp()
    {
        cameraManager.ApplyConfig(campManager.CameraConfig);
    }
}
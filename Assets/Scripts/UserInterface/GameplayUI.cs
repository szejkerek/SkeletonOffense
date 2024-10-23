using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;

    public CampUnitManagmentUI campUnitManagmentUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        campUnitManagmentUI = GetComponentInChildren<CampUnitManagmentUI>();
    }
}

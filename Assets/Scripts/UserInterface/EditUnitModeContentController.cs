using UnityEngine;
using UnityEngine.UI;

public class EditUnitModeContentController : MonoBehaviour
{
    public Button agressiveModeButton;
    public Button basicModeButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agressiveModeButton.onClick.AddListener(SetAgressiveMode);
        basicModeButton.onClick.AddListener(SetBasicMode);
    }

    void SetAgressiveMode()
    {
        Debug.Log("ag");
        GameplayUI.Instance.campUnitManagmentUI.usedArmySlot.GetUnitOnSlot().GetUnitBlueprint().Agressive = true;
    }
    void SetBasicMode()
    {
        Debug.Log("bs");
        GameplayUI.Instance.campUnitManagmentUI.usedArmySlot.GetUnitOnSlot().GetUnitBlueprint().Agressive = false;
    }
}

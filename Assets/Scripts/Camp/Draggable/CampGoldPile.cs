using UnityEngine;

public class CampGoldPile : MonoBehaviour
{
    public DebugText debugText;
    public int amountOfGold = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CampManager.Instance.OnMoneyChange += OnMoneyChange;
        amountOfGold = CampManager.Instance.money;
        debugText.SetText($"{amountOfGold} Gold", Color.yellow);
    }

    // Update is called once per frame
    public void OnMoneyChange(int value)
    {
        //TODO add some fancy animation

        amountOfGold= value;
        debugText.SetText($"{amountOfGold} Gold", Color.yellow);

    }
}

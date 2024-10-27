using UnityEngine;

public class CampGoldPile : MonoBehaviour
{
    public DebugText debugText;
    public int amountOfGold = 0;

    [SerializeField] GameObject goldPilePart1GO;
    [SerializeField] GameObject goldPilePart2GO;
    [SerializeField] GameObject goldPilePart3GO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CampManager.Instance.OnMoneyChange += OnMoneyChange;
        OnMoneyChange(CampManager.Instance.money);
    }

    // Update is called once per frame
    public void OnMoneyChange(int value)
    {
        //TODO add some fancy animation
        //TODO 3 versions of gold pile (based on gold owned)

        amountOfGold= value;
        debugText.SetText($"{amountOfGold} Gold", Color.yellow);


        if (amountOfGold > 50)
        {
            goldPilePart1GO.SetActive(true);
            goldPilePart2GO.SetActive(true);
            goldPilePart3GO.SetActive(true);
        }
        else if (amountOfGold > 20)
        {
            goldPilePart1GO.SetActive(true);
            goldPilePart2GO.SetActive(true);
            goldPilePart3GO.SetActive(false);
        }
        else
        {
            goldPilePart1GO.SetActive(true);
            goldPilePart2GO.SetActive(false);
            goldPilePart3GO.SetActive(false);
        }
    }
}

using UnityEngine;

public class DraggableUnit : MonoBehaviour
{
    private Camera mainCamera;  
    private Vector3 offset;     
    private bool isDragging = false;
    private Vector3 originalPosition;
    
    public UnitBlueprint unitBlueprint;
    public CampBasicSlot currentSlot;    
    public float fixedHeight = 0.5f;

    void Start()
    {
        mainCamera = Camera.main;
        if (currentSlot != null)
        {
            originalPosition = currentSlot.snapPoint.position;
            currentSlot.SetUnitOnSlot(this);
            MoveToSlotPosition();
        }
        else
        {
            originalPosition = transform.position;
        }
    }

    void OnMouseDown()
    {
        if (!UnityDraggingManager.Instance.IsDragging())
        {
            UnityDraggingManager.Instance.StartDragging(this);

            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPos() + offset;
            newPosition.y = fixedHeight;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            UnityDraggingManager.Instance.StopDragging();

            CampSellHole sellHole = CheckForCampSellHole();
            if(sellHole != null)
            {
                int sellPrice = sellHole.CalculateUnitSellPrice(this);
                CampManager.Instance.AddMoney(sellPrice);
                Destroy(gameObject);
                return;
            }

            CampUpgradeHouse upgradeHouse = CheckForCampUpgradeHouse();
            if (upgradeHouse != null)
            {
                int sellPrice = upgradeHouse.CalculateUnitUpgradePrice(this);
                if (CampManager.Instance.TryToBuy(sellPrice))
                    unitBlueprint.LevelUp();

                MoveToSlotPosition();
                return;
            }



            CampBasicSlot validSlot = GetSlotUnderUnit();
            if (validSlot != null)
            {
                if (validSlot is CampArmySlot)
                {
                    if (!(validSlot as CampArmySlot).IsSlotUnLocked()) 
                    {
                        MoveToSlotPosition();
                        return;
                    } 
                }

                if(validSlot.IsSlotOccupied())
                {
                    //zamiana
                    DraggableUnit unitToChangeWith = validSlot.unitOnSlot;
                    currentSlot.SetUnitOnSlot(unitToChangeWith);
                    unitToChangeWith.originalPosition = currentSlot.snapPoint.position;
                    unitToChangeWith.MoveToSlotPosition();

                    currentSlot = validSlot;
                    currentSlot.SetUnitOnSlot(this);

                    originalPosition = currentSlot.snapPoint.position;
                    MoveToSlotPosition();

                }
                else
                {
                    //umieszczenie nowego
                    originalPosition = validSlot.snapPoint.position;
                    MoveToSlotPosition();


                    if (currentSlot != null)
                    {
                        currentSlot.SetUnitOnSlot(null);
                        currentSlot = null;
                    }

                    currentSlot = validSlot;
                    currentSlot.SetUnitOnSlot(this);
                }

                
            }
            else
            {
                MoveToSlotPosition();
            }
        }
    }

    public void MoveToSlotPosition()
    {
        transform.position = originalPosition;
    }

    public UnitBlueprint GetUnitBlueprint()
    {
        return unitBlueprint;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private CampBasicSlot GetSlotUnderUnit()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position , Vector3.down);

        if (Physics.Raycast(ray, out hit, 20f))
        {
            CampBasicSlot slot = hit.collider.GetComponent<CampBasicSlot>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }

    private CampSellHole CheckForCampSellHole()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 20f))
        {
            CampSellHole slot = hit.collider.GetComponent<CampSellHole>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }

    private CampUpgradeHouse CheckForCampUpgradeHouse()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 20f))
        {
            CampUpgradeHouse slot = hit.collider.GetComponent<CampUpgradeHouse>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }

}

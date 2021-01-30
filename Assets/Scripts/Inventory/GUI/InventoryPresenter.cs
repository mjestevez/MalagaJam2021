using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] List<InventoryView> inventoryViews;
    
    int currentSelectedIndex;
    public int CurrentSelectedIndex => currentSelectedIndex;
    void Start()
    {
        currentSelectedIndex = 0;
        inventoryViews[currentSelectedIndex].MarkAsSelected(true);
    }

    public void AddNewObject(int index, Sprite image)
    {
        inventoryViews[index].SetObjectImage(image);
    }

    public bool SelectNewObject(bool isRightInput)
    {
        if(isRightInput && currentSelectedIndex + 1 >= inventoryViews.Count) 
            return false;
        
        if(!isRightInput && currentSelectedIndex - 1 < 0) 
            return false;
        
        inventoryViews[currentSelectedIndex].MarkAsSelected(false);
        currentSelectedIndex += isRightInput ? 1 : -1;
        inventoryViews[currentSelectedIndex].MarkAsSelected(true);
        return true;
    }

    public void RemoveCurrentItem()
    {
        inventoryViews[currentSelectedIndex].SetObjectImage(null);
    }

    public void RemoveItem(int index)
    {
        inventoryViews[index].SetObjectImage(null);
    }
}

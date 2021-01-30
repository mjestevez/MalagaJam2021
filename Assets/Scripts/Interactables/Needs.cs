using Scripts.Inventory;
using UnityEngine;

public class Needs : MonoBehaviour
{
    [SerializeField] Item requiredItem;
    [SerializeField] int amount;

    bool CheckNeedsSupply(InventoryController inventory)
    {
        var items = inventory.FindItemsInInventory(requiredItem);
        return items.Count >= amount;
    }

    public void SupplyNeeds(InventoryController inventory)
    {
        if(CheckNeedsSupply(inventory))
        {
            inventory.RemoveAllItems(requiredItem);
            var color = GetComponent<SpriteRenderer>().color;
            color.a = 1;
            GetComponent<SpriteRenderer>().color = color;
            GetComponent<Collider2D>().enabled = false;
        }
            
    }

}

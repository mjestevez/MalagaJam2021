using System.Collections.Generic;
using Scripts.Inventory;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventoryPresenter presenter;
    
    List<Item> items;

    void Start()
    {
        items = new List<Item>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.gameObject.GetComponent<Item>();
        if(item != null) 
            AddNewItem(item);
    }

    void AddNewItem(Item item)
    {
        items.Add(item);
        presenter.AddNewObject(items.Count-1, item.image);
        item.PickUp();
    }
    
}

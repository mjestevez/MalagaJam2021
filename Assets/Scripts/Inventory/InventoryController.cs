using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public bool isKeyboardControl;
    [SerializeField] InventoryPresenter presenter;
    [SerializeField] int numItemsMax=6;
    [SerializeField] Item initialLanter;

    Item CurrentItem
    {
        get => items[presenter.CurrentSelectedIndex];
        set => items[presenter.CurrentSelectedIndex] = value;
    }

    List<Item> items;
    bool isCurrentItemNull;

    PlayerController playerController => GetComponent<PlayerController>();

    void Start()
    {
        initialLanter = Instantiate(initialLanter);
        items = new List<Item>(numItemsMax)
        {
            null,
            null,
            null,
            null,
            null,
            null
        };
        AddNewItem(initialLanter);
        items.First().Use();
    }
    
    void Update()
    {
        isCurrentItemNull = CurrentItem==null;
        CheckNavigationInputs();
        CheckThrowInputs();
        CheckUseInputs();
    }

    #region Use
    void CheckUseInputs()
    {
        if(!isKeyboardControl)
            ManageGamepadUseInputs();
    }

    void ManageGamepadUseInputs()
    {
        if(Gamepad.current==null)
            return;

        if(Gamepad.current.aButton.wasPressedThisFrame)
            UseCurrentItem();
    }

    void UseCurrentItem()
    {
        if(isCurrentItemNull)
            return;
        
        CurrentItem.Use();
    }
    #endregion

    #region Throw
    void CheckThrowInputs()
    {
        if(!isKeyboardControl)
            ManageGamepadThrowInputs();
    }

    void ManageGamepadThrowInputs()
    {
        if(Gamepad.current==null)
            return;

        if(Gamepad.current.xButton.wasPressedThisFrame)
            ThrowCurrentItem();

    }

    void ThrowCurrentItem()
    { 
        if(isCurrentItemNull)
            return;
        
        CurrentItem.Throw(playerController.lastFaceDirection , playerController.RbForce);
        RemoveSelectedItem();
    }

    void RemoveSelectedItem()
    {
        CurrentItem = null;
        presenter.RemoveCurrentItem();
    }
    #endregion

    #region Navigation
    void CheckNavigationInputs()
    {
        if(isKeyboardControl)
            ManageKeyboardNavigationInputs();
        else
            ManageGamepadNavigationInputs();
    }

    void ManageGamepadNavigationInputs()
    {
        if(Gamepad.current==null)
            return;
        
        if(Gamepad.current.leftShoulder.wasPressedThisFrame)
            SelectNewObject(false);
        if(Gamepad.current.rightShoulder.wasPressedThisFrame)
            SelectNewObject(true);
    }

    void ManageKeyboardNavigationInputs()
    {
        if(Keyboard.current.qKey.wasPressedThisFrame)
            SelectNewObject(false);
        if(Keyboard.current.eKey.wasPressedThisFrame)
            SelectNewObject(true);
    }

    void SelectNewObject(bool isRightInput)
    {
        if(presenter.SelectNewObject(isRightInput) && CurrentItem!=null)
            CurrentItem.SelectItem();
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if(CanPickItems() && other.gameObject.CompareTag($"Item")) 
            AddNewItem(other.gameObject.GetComponent<Item>());
    }

    bool CanPickItems() => items.Any(i => i == null);

    void AddNewItem(Item item)
    {
        var index = items.FindIndex(i => i == null);
        items[index] = item;
        presenter.AddNewObject(index, item.image);
        item.PickUp(transform, index==presenter.CurrentSelectedIndex);
    }
    
}

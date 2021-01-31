﻿using System.Collections.Generic;
using System.Linq;
using Interactables;
using Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InventoryController : MonoBehaviour
{
    bool isKeyboardControl;
    public InventoryPresenter presenter;
    [SerializeField] int numItemsMax=3;
    [SerializeField] LayerMask interactableMask;

    Gamepad device;

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
        items = new List<Item>(numItemsMax)
        {
            null,
            null,
            null,
        };
    }
    
    void Update()
    {
        GetControllerType();
        isCurrentItemNull = CurrentItem==null;
        CheckNavigationInputs();
        CheckThrowInputs();
        CheckUseInputs();
        CheckInteractionInputs();
    }

    void CheckInteractionInputs()
    {
        if(!isKeyboardControl)
            ManageGamepadInteractionInputs();
        else
            ManageKeyboardInteractionInputs();
    }

    void ManageKeyboardInteractionInputs()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame)
            Interaction();
    }

    void ManageGamepadInteractionInputs()
    {
        if(device.yButton.wasPressedThisFrame) 
            Interaction();
    }

    void Interaction()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 1f, interactableMask);
        if(hit != null && hit.GetComponent<Rock>() != null)
            hit.GetComponent<Rock>().Push(CalculateDirection(hit.transform.position));
        if(hit != null && hit.GetComponent<Needs>() != null)
            hit.GetComponent<Needs>().SupplyNeeds(this);
        if(hit != null && hit.GetComponent<Palanca>() != null)
            hit.GetComponent<Palanca>().EnableInteractor();
    }

    Vector2 CalculateDirection(Vector3 hit)
    {
        var direction = hit - transform.position;
        var x = direction.x;
        var y = direction.y;
        if(Mathf.Abs(x) > Mathf.Abs(y))
        {
            return x > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
        }
        else
            return y > 0 ? new Vector2(0, 1) : new Vector2(0, -1);
    }

    void GetControllerType()
    {
        if(Gamepad.all.Count > playerController.playerID)
        {
            device = Gamepad.all[playerController.playerID];
            isKeyboardControl = false;
        }
        else
            isKeyboardControl = true;
    }
    #region Use
    void CheckUseInputs()
    {
        if(!isKeyboardControl)
            ManageGamepadUseInputs();
        else 
            ManageKeyboardUseInputs();
    }

    void ManageKeyboardUseInputs()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
            UseCurrentItem();
    }

    void ManageGamepadUseInputs()
    {
        if(device.aButton.wasPressedThisFrame)
            UseCurrentItem();
    }

    void UseCurrentItem()
    {
        if(isCurrentItemNull)
            return;

        if(CurrentItem.Use() && CurrentItem.isSingleUse) 
            presenter.RemoveCurrentItem();

    }
    #endregion

    #region Throw
    void CheckThrowInputs()
    {
        if(!isKeyboardControl)
            ManageGamepadThrowInputs();
        else
            ManageKeyboardThrowInputs();
    }

    void ManageKeyboardThrowInputs()
    {
        if(Keyboard.current.rKey.wasPressedThisFrame)
            ThrowCurrentItem();
    }

    void ManageGamepadThrowInputs()
    {
        if(device.xButton.wasPressedThisFrame)
            ThrowCurrentItem();
    }

    void ThrowCurrentItem()
    { 
        if(isCurrentItemNull)
            return;
        
        CurrentItem.Throw(playerController.lastFaceDirection , playerController.RbForce);
        RemoveSelectedItem();
    }

    public void RemoveAllItems(Item item)
    {
        var indexs = items.Where(i => i!=null && i.GetType() == item.GetType()).Select(i => items.IndexOf(i)).ToList();
        foreach(var i in indexs)
        {
            items[i] = null;
            presenter.RemoveItem(i);
        }
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
        if(device.leftShoulder.wasPressedThisFrame)
            SelectNewObject(false);
        if(device.rightShoulder.wasPressedThisFrame)
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
        if(CanPickItems() && other.gameObject.CompareTag("Item")) 
            AddNewItem(other.gameObject.GetComponent<Item>());
    }

    bool CanPickItems() => items.Any(i => i == null);

    void AddNewItem(Item item)
    {
        var index = items.FindIndex(i => i == null);
        items[index] = item;
        presenter.AddNewObject(index, item.image, item.color);
        item.PickUp(transform, index==presenter.CurrentSelectedIndex);
    }

    public List<Item> FindItemsInInventory(Item item)
    {
        return items.Where(i => i!=null && i.GetType() == item.GetType()).ToList();
    }
    
}

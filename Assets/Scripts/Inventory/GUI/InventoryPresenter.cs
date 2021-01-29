using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryPresenter : MonoBehaviour
{
    public bool isKeyboardControl;
    [SerializeField] List<InventoryView> inventoryViews;

    int currentSelectedIndex;

    void Start()
    {
        currentSelectedIndex = 0;
        inventoryViews[currentSelectedIndex].MarkAsSelected(true);
    }

    void Update()
    {
        ManageController();
    }

    void ManageController()
    {
        if(isKeyboardControl)
            ManageKeyboardController();
        else
            ManageGamepadController();
    }

    void ManageGamepadController()
    {
        if(Gamepad.current==null)
            return;
        
        if(Gamepad.current.leftShoulder.wasPressedThisFrame)
            SelectNewObject(false);
        if(Gamepad.current.rightShoulder.wasPressedThisFrame)
            SelectNewObject(true);
    }

    void ManageKeyboardController()
    {
        if(Keyboard.current.qKey.wasPressedThisFrame)
            SelectNewObject(false);
        if(Keyboard.current.eKey.wasPressedThisFrame)
            SelectNewObject(true);
    }

    public void AddNewObject(int index, Sprite image)
    {
        inventoryViews[index].SetObjectImage(image);
    }
    void SelectNewObject(bool isRightInput)
    {
        if(isRightInput && currentSelectedIndex + 1 >= inventoryViews.Count) 
            return;
        
        if(!isRightInput && currentSelectedIndex - 1 < 0) 
            return;
        
        inventoryViews[currentSelectedIndex].MarkAsSelected(false);
        currentSelectedIndex += isRightInput ? 1 : -1;
        inventoryViews[currentSelectedIndex].MarkAsSelected(true);
    }
}

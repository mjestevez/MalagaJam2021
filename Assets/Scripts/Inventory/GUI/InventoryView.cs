using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] GameObject selection;
    [SerializeField] Image item;

    public void MarkAsSelected(bool selected)
    {
        selection.SetActive(selected);
    }

    public void SetObjectImage(Sprite image)
    {
        item.gameObject.SetActive(image!=null);
        item.sprite = image;
    }
}
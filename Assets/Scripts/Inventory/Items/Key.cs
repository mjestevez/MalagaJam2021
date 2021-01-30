using Scripts.Inventory;
using UnityEngine;

public class Key : Item
{
    [SerializeField] Door door;
    [SerializeField] float distanceToUse;

    public override bool Use()
    {
        base.Use();
        if(Vector3.Distance(door.transform.position, transform.position) < distanceToUse)
        {
            door.Unlock();
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}

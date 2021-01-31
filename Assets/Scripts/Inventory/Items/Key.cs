using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Inventory;
using UnityEngine;

public class Key : Item
{
    [SerializeField] List<Door> doors;
    [SerializeField] float distanceToUse;

    public override bool Use()
    {
        base.Use();
        foreach(var door in doors.Where(d => Vector2.Distance(d.transform.position, transform.position) < distanceToUse))
        {
            door.Unlock();
            Destroy(gameObject);
            return true;
        }
        
        return false;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var rb = GetComponent<Rigidbody2D>();
        if(other.gameObject.CompareTag("Wall")) 
            rb.velocity = -rb.velocity;

    }
}

using System.Linq;
using Interactables;
using Scripts.Inventory;
using UnityEngine;

public class Axe : Item
{
    [SerializeField] float distanceToUse;
    [SerializeField] Item woodItem;

    public override bool Use()
    {
        base.Use();
        var hits = Physics2D.OverlapCircleAll(transform.position, distanceToUse);
        if(hits.Any(i => i.GetComponent<Wood>() != null))
        {
            var wood = hits.First(i => i.GetComponent<Wood>() != null).GetComponent<Wood>();
            wood.Chop();

            Instantiate(woodItem, wood.transform.position, Quaternion.identity);
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

using UnityEngine;

namespace Scripts.Inventory
{
    public class WoodItem : Item
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = GetComponent<Rigidbody2D>();
            if(other.gameObject.CompareTag("Wall")) 
                rb.velocity = -rb.velocity;
        }
    }
}
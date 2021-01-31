using UnityEngine;

namespace Scripts.Inventory
{
    public class Potion : Item
    {
        [SerializeField] float recoveryAmount;
        public override bool Use()
        {
            base.Use();
            var health = GetComponentInParent<HealthController>();
            health.DenigranciaDestroy_LocalTeam(recoveryAmount);
            Destroy(gameObject);
            return true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = GetComponent<Rigidbody2D>();
            if(other.gameObject.CompareTag("Wall")) 
                rb.velocity = -rb.velocity;
        }
    }
}
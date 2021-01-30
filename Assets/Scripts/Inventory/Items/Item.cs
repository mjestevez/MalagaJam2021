using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Scripts.Inventory
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] float force = 5f;
        [SerializeField] float cooldown = 0.1f;
        public Sprite image;
        public bool isSingleUse;

        Rigidbody2D rb;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        public virtual void PickUp(Transform target, bool isCurrentSelected)
        {
            transform.SetParent(target);
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        public virtual void SelectItem(){}

        public virtual bool Use()
        {
            return true;
        }

        public virtual void Throw(Vector3 direction, float rbForce)
        {
            transform.SetParent(null);
            gameObject.SetActive(true);
            
            StopAllCoroutines();
            StartCoroutine(ColliderCooldown());

            var impulseDirection = rbForce != 0 
                ? direction * (force * Mathf.Abs(rbForce)) 
                : direction * force;
            if(rb == null)
                rb = GetComponent<Rigidbody2D>();
            rb.AddForce(impulseDirection, ForceMode2D.Impulse);
        }
        

        IEnumerator ColliderCooldown()
        {
            gameObject.tag = "Untagged";
            yield return new WaitForSeconds(cooldown);
            gameObject.tag = "Item";
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag($"Wall")) 
                rb.velocity = -rb.velocity;

        }
    }
}
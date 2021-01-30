using UnityEngine;

namespace Scripts.Inventory
{
    public class Lantern : Item
    {
        GameObject itemFov;

        bool isActive;

        void Awake()
        {
            UpdateFovItem();
        }

        void UpdateFovItem()
        {
            if(itemFov == null)
            {
                itemFov = GetComponentInChildren<FieldOfView>().gameObject;
                itemFov.transform.SetParent(null);
                itemFov.transform.position= Vector3.zero;
                itemFov.gameObject.SetActive(false);
            }
        }

        public override void PickUp(Transform target, bool isCurentSelected)
        {
            base.PickUp(target, isCurentSelected);
            UpdateFovItem();
            itemFov.gameObject.SetActive(false);
            if(isCurentSelected) 
                SelectItem();
        }

        public override bool Use()
        {
            var active = fov.activeInHierarchy;
            ActivateFOV(!active);
            isActive = !active;
            return true;
        }

        public override void SelectItem()
        {
            UpdateFovItem();
            ActivateFOV(isActive);
        }

        public override void Throw(Vector3 direction, float rbForce)
        {
            base.Throw(direction, rbForce);
            ActivateFOV(false);
            itemFov.gameObject.SetActive(isActive);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = GetComponent<Rigidbody2D>();
            if(other.gameObject.CompareTag("Wall")) 
                rb.velocity = -rb.velocity;

        }
        
    }
}
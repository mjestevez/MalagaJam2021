﻿using UnityEngine;

namespace Scripts.Inventory
{
    public class Lantern : Item
    {
        public Sprite off;
        public Sprite on;
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
                itemFov.transform.localScale = Vector3.one;
                itemFov.gameObject.SetActive(false);
            }
        }

        public override void PickUp(Transform target, bool isCurentSelected)
        {
            base.PickUp(target, isCurentSelected);
            UpdateFovItem();
            itemFov.gameObject.SetActive(false);
            GetComponentInParent<InventoryController>().presenter.SetObjectImage(!isActive ? off : on);
            if(isCurentSelected) 
                SelectItem();
        }

        public override bool Use()
        {
            var active = fov.activeInHierarchy;
            GetComponentInParent<InventoryController>().presenter.SetObjectImage(active ? off : on);
            ActivateFOV(!active);
            isActive = !active;
            return true;
        }

        public override void SelectItem()
        {
            UpdateFovItem();
            ActivateFOV(isActive);
        }

        public override void Throw(Vector3 plogic, float rbForce)
        {
            base.Throw(plogic, rbForce);
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
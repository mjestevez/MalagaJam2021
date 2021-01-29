using UnityEngine;

namespace Scripts.Inventory
{
    public class Lantern : Item
    {
        GameObject fov;
        GameObject baseFov;

        bool isActive;

        void Awake()
        {
            fov = FindObjectOfType<PlayerController>().fov.gameObject;
            baseFov = FindObjectOfType<PlayerController>().baseFov.gameObject;
        }

        public override void PickUp(Transform target, bool isCurentSelected)
        {
            base.PickUp(target, isCurentSelected);
            if(isCurentSelected) 
                SelectItem();
        }

        public override void Use()
        {
            base.Use();
            var active = fov.activeInHierarchy;
            ActivateFOV(!active);
            isActive = !active;
        }

        public override void SelectItem()
        {
            base.SelectItem();
            ActivateFOV(isActive);
        }

        public override void Throw(Vector3 direction, float rbForce)
        {
            base.Throw(direction, rbForce);
            ActivateFOV(false);
        }

        void ActivateFOV(bool active)
        {
            fov.SetActive(active);
            baseFov.SetActive(!active);
        }
        
    }
}
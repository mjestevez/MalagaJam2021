using UnityEngine;

namespace Scripts.Inventory
{
    public class Lantern : Item
    {
        GameObject fov;
        GameObject baseFov;
        GameObject itemFov;

        bool isActive;

        void Awake()
        {
            UpdateFovReferences();
        }
        void UpdateFovReferences()
        {
            var controller = GetComponentInParent<PlayerController>();
            if(controller != null)
            {
                fov = controller.fov.gameObject;
                baseFov = controller.baseFov.gameObject;
            }

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
            UpdateFovReferences();
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
            base.SelectItem();
            ActivateFOV(isActive);
        }

        public override void Throw(Vector3 direction, float rbForce)
        {
            base.Throw(direction, rbForce);
            ActivateFOV(false);
            itemFov.gameObject.SetActive(isActive);
        }

        void ActivateFOV(bool active)
        {
            fov.SetActive(active);
            baseFov.SetActive(!active);
        }
        
    }
}
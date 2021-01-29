using UnityEngine;

namespace Scripts.Inventory
{
    public abstract class Item : MonoBehaviour
    {
        public Sprite image;
        
        public void PickUp()
        {
            Destroy(gameObject);
        }

        public void Use() {}

        public void Throw()
        {
            
        }
    }
}
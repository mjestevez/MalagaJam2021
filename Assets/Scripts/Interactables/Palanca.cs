using UnityEngine;

namespace Interactables
{
    public class Palanca : MonoBehaviour
    {
        [SerializeField] GameObject off;
        [SerializeField] GameObject on;
        [SerializeField] Door door;
        
        
        public void EnableInteractor()
        {
            door.Unlock();
            off.SetActive(false);
            on.SetActive(true);
            gameObject.layer = 0;
        }
        
    }
}
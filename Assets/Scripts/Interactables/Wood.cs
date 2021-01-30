using UnityEngine;

namespace Interactables
{
    public class Wood : MonoBehaviour
    {
        public void Chop()
        {
            Destroy(gameObject);
        }
    }
}
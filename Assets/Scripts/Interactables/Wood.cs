using UnityEngine;

namespace Interactables
{
    public class Wood : MonoBehaviour
    {
        public void Chop()
        {
            gameObject.SetActive(false);
            Destroy(gameObject,1f);
        }
    }
}
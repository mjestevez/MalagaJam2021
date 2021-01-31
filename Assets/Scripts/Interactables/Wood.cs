using KrillAudio.Krilloud;
using UnityEngine;

namespace Interactables
{
    public class Wood : MonoBehaviour
    {
        public void Chop()
        {
            GetComponent<KLAudioSource>().Play();
            gameObject.SetActive(false);
            Destroy(gameObject,1f);
        }
    }
}
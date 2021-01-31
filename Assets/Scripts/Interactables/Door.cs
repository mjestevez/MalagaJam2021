using KrillAudio.Krilloud;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public void Unlock()
    {
        gameObject.SetActive(false);
        GetComponent<KLAudioSource>().Play();
    }

    public void Lock()
    {
        gameObject.SetActive(true);
    }
}

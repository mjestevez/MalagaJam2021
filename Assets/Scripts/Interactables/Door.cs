using UnityEngine;

public class Door : MonoBehaviour
{
    public void Unlock()
    {
        gameObject.SetActive(false);
    }

    public void Lock()
    {
        gameObject.SetActive(true);
    }
}

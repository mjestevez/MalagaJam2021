using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] float maxTimeSeconds;
    [SerializeField] Image healthBar;
    float currentTime;

    void Start()
    {
        currentTime = maxTimeSeconds;
    }

    void Update()
    {
        if(currentTime >= 0)
        {
            healthBar.fillAmount = currentTime / maxTimeSeconds;
            currentTime -= Time.deltaTime;
            if(currentTime < 0) currentTime = 0;
        }
    }

    public void DenigranciaDestroy_LocalTeam(float amount)
    {
        currentTime += amount;
    }
}

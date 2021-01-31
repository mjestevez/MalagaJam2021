using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public GameObject UI;
    public GameObject FOV;
    [SerializeField] float maxTimeSeconds;
    [SerializeField] Image healthBar;
    public float currentTime;
    [SerializeField] Image icon;
    public List<Sprite> icons;

    void Start()
    {
        currentTime = maxTimeSeconds;
        icon.sprite = icons[0];
    }

    void Update()
    {
        if(currentTime >= 0)
        {
            healthBar.fillAmount = currentTime / maxTimeSeconds;
            currentTime -= Time.deltaTime;
            if(currentTime < 0) 
                currentTime = 0;

            UpdateIcon();
        }
    }

    void UpdateIcon()
    {
        if(currentTime >= maxTimeSeconds / 2) 
            icon.sprite = icons[0];
        if(currentTime < maxTimeSeconds / 2) 
            icon.sprite = icons[1];
        if(currentTime <= 0) 
            icon.sprite = icons[2];
    }

    public void DenigranciaDestroy_LocalTeam(float amount)
    {
        if(currentTime>0)
            currentTime += amount;
    }
}

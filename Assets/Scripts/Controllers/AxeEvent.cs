using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class AxeEvent : MonoBehaviour
    {
        [SerializeField] GameObject wood;
        [SerializeField] GameObject woodN;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                Destroy(wood.gameObject);
                woodN.SetActive(true);
                Camera.main.DOShakePosition(0.5f);
                Destroy(this.gameObject);
            }

        }
    }
}
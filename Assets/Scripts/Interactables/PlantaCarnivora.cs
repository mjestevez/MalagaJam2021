using System;
using Scripts.Inventory;
using UnityEngine;

namespace Interactables
{
    public class PlantaCarnivora : MonoBehaviour
    {
        bool active = true;

        void OnCollisionEnter2D(Collision2D other)
        {
            if(active)
            {
                if(other.collider.CompareTag("Player"))
                {
                    active = false;
                    other.collider.GetComponent<HealthController>().UI.SetActive(false);
                    other.collider.GetComponent<HealthController>().FOV.SetActive(false);
                    other.collider.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }

            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(active)
            {
                if(other.CompareTag("Item"))
                {
                    if(other.GetComponent<Pig>() != null)
                    {
                        active = false;
                        other.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                    }
                
                }
            }
            
        }
    }
}
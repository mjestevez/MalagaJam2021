using System;
using UnityEngine;

namespace Interactables
{
    public class RockGenerator : MonoBehaviour
    {
        [SerializeField] GameObject rock;

        Vector3 pos;

        void Start()
        {
            pos = rock.transform.position;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                rock.transform.position = pos;
        }
    }
}
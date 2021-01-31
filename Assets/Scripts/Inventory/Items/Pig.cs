using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Inventory
{
    public class Pig : Item
    {
        [SerializeField] int nCollide;
        [SerializeField] float speed;
        [SerializeField] List<Vector2> directions;

        bool isRunning;
        Vector2 logic;
        int collisions;
        Rigidbody2D rb2D;

        void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        public override void Throw(Vector3 plogic, float rbForce)
        {
            transform.SetParent(null);
            gameObject.SetActive(true);
            
            StopAllCoroutines();
            StartCoroutine(ColliderCooldown());

            isRunning = true;
            rb2D.velocity = plogic * speed;
            collisions = 0;
            logic = plogic;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if(isRunning && other.CompareTag("Wall")) 
                StartCoroutine(ChangeDirection());

        }
        
        IEnumerator ChangeDirection()
        {
            rb2D.velocity = -rb2D.velocity;
            yield return new WaitForSeconds(0.1f);
            logic = GenerateRandomDirection();
            rb2D.velocity = logic * speed;
            collisions++;
            if(collisions >= nCollide)
                StopRunning();
        }
        public override void PickUp(Transform target, bool isCurrentSelected)
        {
            base.PickUp(target, isCurrentSelected);
            StopRunning();
        }

        void StopRunning()
        {
            rb2D.velocity = Vector2.zero;
            collisions = 0;
            isRunning = false;

        }

        Vector2 GenerateRandomDirection()
        {
            var availableDirections= directions.Where(d => d != logic).ToList();
            return availableDirections[Random.Range(0, availableDirections.Count)];
        }
    }
}
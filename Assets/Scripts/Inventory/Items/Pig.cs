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
        Vector2 lastDirection;
        int collisions;
        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public override void Throw(Vector3 direction, float rbForce)
        {
            transform.SetParent(null);
            gameObject.SetActive(true);
            
            StopAllCoroutines();
            StartCoroutine(ColliderCooldown());

            isRunning = true;
            rb.velocity = direction * speed;
            collisions = 0;
            lastDirection = direction;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if(isRunning && other.CompareTag("Wall")) 
                StartCoroutine(ChangeDirection());

        }
        
        IEnumerator ChangeDirection()
        {
            rb.velocity = -rb.velocity;
            yield return new WaitForSeconds(0.1f);
            lastDirection = GenerateRandomDirection();
            rb.velocity = lastDirection * speed;
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
            rb.velocity = Vector2.zero;
            collisions = 0;
            isRunning = false;

        }

        Vector2 GenerateRandomDirection()
        {
            var availableDirections= directions.Where(d => d != lastDirection).ToList();
            return availableDirections[Random.Range(0, availableDirections.Count)];
        }
    }
}
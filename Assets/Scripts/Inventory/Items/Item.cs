using System.Collections;
using KrillAudio.Krilloud;
using UnityEngine;

namespace Scripts.Inventory
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] float force = 5f;
        [SerializeField] float cooldown = 0.1f;
        public Sprite image;
        public Color color = Color.white;
        public bool isSingleUse;
        
        protected GameObject fov;
        protected GameObject baseFov;

        Rigidbody2D rb;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            UpdateFovReferences();
        }
        
        public virtual void PickUp(Transform target, bool isCurrentSelected)
        {
            transform.SetParent(target);
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        public virtual void SelectItem()
        {
            ActivateFOV(false);
        }

        public virtual bool Use()
        {
            return true;
        }

        public virtual void Throw(Vector3 plogic, float rbForce)
        {
            transform.SetParent(null);
            gameObject.SetActive(true);
            
            StopAllCoroutines();
            StartCoroutine(ColliderCooldown());

            var impulseDirection = plogic * (force * Mathf.Abs(5f));
            if(rb == null)
                rb = GetComponent<Rigidbody2D>();
            rb.AddForce(impulseDirection, ForceMode2D.Impulse);
        }


        protected IEnumerator ColliderCooldown()
        {
            gameObject.tag = "Untagged";
            yield return new WaitForSeconds(cooldown);
            gameObject.tag = "Item";
        }
        

        void UpdateFovReferences()
        {
            var controller = GetComponentInParent<PlayerController>();
            if(controller != null)
            {
                fov = controller.fov.gameObject;
                baseFov = controller.baseFov.gameObject;
            }

        }
        
        protected void ActivateFOV(bool active)
        {
            UpdateFovReferences();
            fov.SetActive(active);
            baseFov.SetActive(!active);
            
            var audioSource = GetComponent<KLAudioSource>();
            if(audioSource==null)
                return;
            
            if(active)
                audioSource.Play();
            else
                audioSource.Stop();
        }
    }
}
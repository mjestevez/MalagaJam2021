using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] bool activeByPlayer;
    [SerializeField] bool isSingleUse;
    [SerializeField] List<Interactor> dependences;
    [SerializeField] Door door;

    public bool isActive;
    string objectTag;

    void Start()
    {
        objectTag = activeByPlayer ? "Player" : "Wall";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(objectTag) && !isActive)
            EnableInteractor();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(objectTag) && !isSingleUse)
            DisableInteractor();
    }

    void EnableInteractor()
    {
        isActive = true;
        CheckDependences();
    }

    void DisableInteractor()
    {
        isActive = false;
        if(!isSingleUse)
            door.Lock();
    }
    
    void CheckDependences()
    {
        if(dependences.Count==0)
            door.Unlock();
        else
        {
            if(dependences.All(d => d.isActive))
                door.Unlock();
        }
        
    }
}

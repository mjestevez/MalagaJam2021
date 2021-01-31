using System;
using UnityEngine;

namespace Controllers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] CameraController cameraController;
        [SerializeField] bool needPlayerOne;
        [SerializeField] bool needPlayerTwo;
        public bool playerOneActive;
        public bool playerTwoActive;

        void OnCollisionEnter2D(Collision2D other)
        {
            if(other.collider.CompareTag("Player"))
            {
                var id = other.collider.GetComponent<PlayerController>().playerID;
                if(needPlayerOne && id==0)
                    playerOneActive = true;
                if(needPlayerTwo && id==1)
                    playerTwoActive = true;

                cameraController.CheckDependences();
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if(playerOneActive)
                playerOneActive = false;
            if(playerTwoActive)
                playerTwoActive = false;
        }

        public bool IsActive()
        {
            return needPlayerOne == playerOneActive && needPlayerTwo == playerTwoActive;
        }
    }
}
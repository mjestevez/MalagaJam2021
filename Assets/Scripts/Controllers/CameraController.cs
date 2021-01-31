using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Vector3 cameraPos;
        [SerializeField] Vector3 player1Pos;
        [SerializeField] Vector3 player2Pos;
        [SerializeField] PlayerController player1Controller;
        [SerializeField] PlayerController player2Controller;
        [SerializeField] List<CameraManager> dependences;

        bool movingCamera;

        public void CheckDependences()
        {
            if(dependences.All(d => d.IsActive()))
            {
                if(!movingCamera)
                {
                    movingCamera = true;
                    MoveCamera();
                }
                
            }
        }

        void MoveCamera()
        {
            player1Controller.enabled = false;
            player2Controller.enabled = false;
            player1Controller.transform.position = player1Pos;
            player2Controller.transform.position = player2Pos;
            Camera.main.transform.DOMove(cameraPos, 2f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    player1Controller.enabled = true;
                    player2Controller.enabled = true;
                    movingCamera = false;
                }).Play();
        }
    }
}
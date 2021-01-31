using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDejuego : MonoBehaviour
{
    public HealthController player1;
    public HealthController player2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var player1Alive = player1 != null;
            var player2Alive = player2 != null;

            var player1Human=false;
            var player2Human= false;

            if(player1Alive)
                player1Human = player1.currentTime > 0;
            if(player2Alive)
                player2Human = player2.currentTime > 0;

            if(player1Alive && player2Alive)
            {
                if(player1Human && !player2Human)
                    SceneManager.LoadScene("Final Bueno H C");
                else if(!player1Human && player2Human)
                    SceneManager.LoadScene("Final Bueno M C");
                else
                    SceneManager.LoadScene("Final Bueno H M");
            }
            else if(!player1Alive)
                SceneManager.LoadScene("Final Bueno M");
            else
                SceneManager.LoadScene("Final Bueno H");
        }
    }

    void Update()
    {
        if(player1.currentTime <= 0 && player2.currentTime <= 0)
        {
            SceneManager.LoadScene("Final Malo");
        }
    }
}

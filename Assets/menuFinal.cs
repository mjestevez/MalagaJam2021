using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuFinal : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(BacktoMenu),5f);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("MENU");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CambioScene : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }
}
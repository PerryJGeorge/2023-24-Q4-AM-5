using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flick : MonoBehaviour
{
    public Scene_change fade;
    public string Load;
    public float wait;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("LoadScene",wait);
        fade.leave();
       
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(Load);
    }

}

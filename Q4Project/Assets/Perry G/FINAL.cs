using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FINAL : MonoBehaviour
{
    public Scene_change fade;
    public string Load;
    public float wait;
    public void Start()
    {
        Invoke("LoadScene", wait);
        fade.leave();
    }

        private void LoadScene()
    {
        SceneManager.LoadScene(Load);
    }
}

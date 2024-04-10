using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_change : MonoBehaviour
{
    public void leave()
    {
        GetComponent<Animator>().SetTrigger("leave");
    }
}

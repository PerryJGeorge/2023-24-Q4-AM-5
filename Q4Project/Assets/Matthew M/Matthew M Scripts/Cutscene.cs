using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public PlayerCtrl player;
    public DialogueTriggerScript DTS;
    public GameObject scenechange;

    // Start is called before the first frame update
    void Start()
    {
        player.movSpeed = 0;
        DTS.TriggerDialogue();
        scenechange.GetComponent<flick>().UniqueExit();
        player.movSpeed = 5;
    }
}

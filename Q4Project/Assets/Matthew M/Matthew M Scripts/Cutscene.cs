using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public PlayerCtrl player;
    public DialogueTriggerScript DTS;
    public GameObject scenechange;

    void Start()
    {
        player.movSpeed = 0;
        DTS.TriggerDialogue();
        scenechange.GetComponent<flick>().UniqueExit();
        player.movSpeed = 10;
    }
}

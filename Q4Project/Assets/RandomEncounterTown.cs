using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterTown : MonoBehaviour
{
    public flick loadbattlescene;

//    void Start()
//    {
//        while (true)
//        {
//            StartCoroutine(CheckForScene());
//        }
//    }

    IEnumerator CheckForScene()
    {
        yield return new WaitForSeconds(1f);
        if (Random.Range(1, 101) <= 10)
        {
            loadbattlescene.UniqueExit();
        }
    }
}

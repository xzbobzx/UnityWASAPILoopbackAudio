using Assets.Scripts;
using Assets.Scripts.Extensions;
using Assets.Scripts.ReactiveEffects.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private LoopbackAudio _loopbackAudio;
    public int index;
    public int indexMin;
    public int indexMax;

    // Start is called before the first frame update
    void Start()
    {
        _loopbackAudio = FindObjectOfType<LoopbackAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        int amount = indexMax - indexMin;
        float counter = 0;

        for( int i = 0; i < amount; i++ )
        {
            counter += _loopbackAudio.PostScaledSpectrumData[indexMin + i];
        }

        counter = counter / amount;

        transform.localScale = Vector3.one * counter;

        //transform.localScale = Vector3.one * _loopbackAudio.PostScaledSpectrumData[index];
    }
}


/* spectrum of 64:

    200hz = 2
    300hz = 3
    1000hz = 12
    3400hz = 32
    10000hz = 56
    15000hz = 62
*/

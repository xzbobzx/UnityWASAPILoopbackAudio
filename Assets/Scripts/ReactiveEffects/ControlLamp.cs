using Assets.Scripts;
using Assets.Scripts.Extensions;
using Assets.Scripts.ReactiveEffects.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLamp : MonoBehaviour
{
    private LoopbackAudio _loopbackAudio;

    public float multiplier;
    public int indexMin;
    public int indexMax;

    public float attack;
    public float decay;
    private float previousCounter = 0;

    public enum RangeState { average, min, max };
    public RangeState rangeState;

    public Light lamp;

    // Start is called before the first frame update
    void Start()
    {
        _loopbackAudio = FindObjectOfType<LoopbackAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Application.targetFrameRate * Time.deltaTime;

        int amount = indexMax - indexMin;
        float counter = 0;

        if( rangeState == RangeState.average )
        {
            for( int i = 0; i < amount; i++ )
            {
                counter += _loopbackAudio.PostScaledSpectrumData[indexMin + i];
            }

            counter = counter / amount;
        }
        else if( rangeState == RangeState.min )
        {
            counter = Mathf.Infinity;

            for( int i = 0; i < amount; i++ )
            {
                float audio = _loopbackAudio.PostScaledSpectrumData[indexMin + i];

                if( audio < counter )
                {
                    counter = audio;
                }
            }
        }
        else if( rangeState == RangeState.max )
        {
            for( int i = 0; i < amount; i++ )
            {
                float audio = _loopbackAudio.PostScaledSpectrumData[indexMin + i];

                if( audio > counter )
                {
                    counter = audio;
                }
            }
        }

        counter = counter * multiplier;

        if( counter > previousCounter )
        {
            lamp.intensity = Mathf.Lerp( lamp.intensity, counter, attack * time );
        }
        else
        {
            lamp.intensity = Mathf.Lerp( lamp.intensity, counter, decay * time );
        }

        //transform.localScale = Vector3.one * counter * multiplier;

        //transform.localScale = Vector3.one * _loopbackAudio.PostScaledSpectrumData[index];

        previousCounter = counter;
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

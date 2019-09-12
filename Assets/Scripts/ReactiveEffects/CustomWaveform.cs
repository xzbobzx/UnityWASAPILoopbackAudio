using Assets.Scripts;
using Assets.Scripts.Extensions;
using Assets.Scripts.ReactiveEffects.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWaveform : MonoBehaviour
{
    private LoopbackAudio loopbackAudio;

    public Transform bounds;
    public Transform container;
    public GameObject prefab;
    private List<GameObject> bars = new List<GameObject>();

    public enum RangeState { average, min, max };
    public RangeState rangeState;

    public Color start;
    public Color end;

    private void Awake()
    {
        loopbackAudio = FindObjectOfType<LoopbackAudio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        container.eulerAngles = Vector3.zero;

        for( int i = 0; i < loopbackAudio.SpectrumSize - 1; i++ )
        {
            GameObject newBar = Instantiate( prefab );
            newBar.transform.parent = container;

            float size = (float)1 / ( loopbackAudio.SpectrumSize - 1 );

            newBar.transform.localScale = new Vector3( size, 0, 1 );
            newBar.transform.position = container.TransformPoint( ( size * i ) + ( size * 0.5f ) - (bounds.localScale.x * 0.5f), 0, 0 );

            Renderer rend = newBar.GetComponent<Renderer>();
            float colorrr = Mathf.InverseLerp( 0, loopbackAudio.SpectrumSize - 1, i);

            Debug.Log( colorrr );

            Color color = Color.Lerp( start, end, colorrr );
            rend.material.SetColor( "_Color", color );
            rend.material.SetColor( "_EmissionColor", color );

            bars.Add( newBar );
        }

        container.eulerAngles = transform.eulerAngles;
        bounds.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Application.targetFrameRate * Time.deltaTime;

        for( int i = 0; i < loopbackAudio.PostScaledSpectrumData.Length - 1; i++ )
        {
            float size = Mathf.InverseLerp( 0, 10, loopbackAudio.PostScaledSpectrumData[i + 1] );

            if( size < 0.01f )
            {
                size = 0;
                bars[i].SetActive( false );
            }
            else
            {
                bars[i].SetActive( true );
            }

            bars[i].transform.localScale = new Vector3( bars[i].transform.localScale.x, size, bars[i].transform.localScale.z );
        }
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

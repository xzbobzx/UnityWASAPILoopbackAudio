using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CommunicateWithVLC : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float ticker = 1;

    public string artist;
    public string title;
    public string filename;

    void Start()
    {
        //StartCoroutine( MakeRequest() );
    }

    private void Update()
    {
        ticker += Time.deltaTime;
        
        if( ticker > 1 )
        {
            StartCoroutine( MakeRequest() );
            //Debug.Log( "update" );

            ticker = 0;
        }
    }

    string Authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String( System.Text.Encoding.GetEncoding( "ISO-8859-1" ).GetBytes( auth ) );
        auth = "Basic " + auth;
        return auth;
    }

    void DoXML(DownloadHandler leXML)
    {
        XmlReader xmlReader = XmlReader.Create( new System.IO.StringReader( leXML.text ) );

        filename = "";
        title = "";
        artist = "";

        try
        {
            while( xmlReader.Read() )
            {
                if( ( xmlReader.NodeType == XmlNodeType.Element ) && ( xmlReader.Name == "info" ) )
                {
                    //Debug.Log( xmlReader.GetAttribute( xmlReader.AttributeCount - 1 ) );

                    if( xmlReader.GetAttribute( xmlReader.AttributeCount - 1 ) == "title" ) // bug: sometimes doesn't read title??
                    {
                        title = xmlReader.ReadElementContentAsString();
                    }

                    if( xmlReader.GetAttribute( xmlReader.AttributeCount - 1 ) == "artist" )
                    {
                        artist = xmlReader.ReadElementContentAsString();
                    }

                    if( xmlReader.GetAttribute( xmlReader.AttributeCount - 1 ) == "filename" )
                    {
                        filename = xmlReader.ReadElementContentAsString();
                    }
                }
            }
        }
        catch
        {
        }

        /*if( title != "" & artist != "" )
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append( artist );
            sb.Append( " - " );
            sb.Append( title );

            text.text = sb.ToString();
        }
        else */
        if( filename != "" )
        {
            text.text = RestSharp.Contrib.HttpUtility.HtmlDecode( filename );
        }
        else
        {
            text.text = "...";
        }
    }

    IEnumerator MakeRequest()
    {
        string authorization = Authenticate( "", "password" );
        string url = "http://127.0.0.1:8080/requests/status.xml";

        UnityWebRequest www = UnityWebRequest.Get( url );
        www.SetRequestHeader( "AUTHORIZATION", authorization );

        yield return www.SendWebRequest();

        if( www.isNetworkError || www.isHttpError )
        {
            //Debug.Log( www.error );

            text.text = "...";
        }
        else
        {
            DoXML( www.downloadHandler );
        }
    }
}

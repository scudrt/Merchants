using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class client : MonoBehaviour
{
    
    private byte reliableChannel;
    private const int MAX_USER = 100;

    private int PORT = 26000;
    private int WEB_PORT = 26001;

    private int hostId;
    private int webHostId;
    private int connectionId;
   
    private bool isStarted = false;

    private const string SERVER_IP = "127.0.0.1";
    private byte error;

    private const int BYTE_SIZE = 1024;
    // Start is called before the first frame update
    #region Monobehaviour
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }
    

    private void Update()
    {
        UpdateMessgaePump();
    }
    #endregion
    public void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        cc.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        //client only code
        hostId = NetworkTransport.AddHost(topo, 0);
#if UNITY_EDITOR || !UNITY_WEBGL
        //Stand alone client
        Debug.Log("connecting from standalone");
        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, PORT, 0, out error);

#else
        //web client
        Debug.Log("connecting from web client");
        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, WEB_PORT, 0, out error);
#endif
        
        Debug.Log(string.Format("Attemping to connect on {0}...", SERVER_IP));

        isStarted = true;
    }

    public void ShutDown()
    {
        NetworkTransport.Shutdown();
        isStarted = false;
        Debug.Log("network shutdown");
    }
    // Update is called once per frame

    public void sendServer()
    {

    }
    public void UpdateMessgaePump()
    {
        if (!isStarted)
        {
            return;
        }
        int recHostId; //web client or standalone
        int connectionId;// which user
        int channelId;//which lane or channel

        byte[] recBuffer = new byte[BYTE_SIZE];
        int dataSize;
        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, BYTE_SIZE, out dataSize, out error);

        switch (type)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("We have been connected to the server");
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log("We have been disconnected to the server");
                break;

            case NetworkEventType.BroadcastEvent:
                Debug.Log("Broadcast event happened");
                break;

            case NetworkEventType.DataEvent:
                Debug.Log("data");
                break;
            default:
                break;
        }
    }
}

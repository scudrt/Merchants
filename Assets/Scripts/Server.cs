using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
struct user
{
    public int recHostId;
    public int channelId;
    public int connectionId;
}
public class Server:MonoBehaviour
{
    //block位置静态
    private List<user> users = new List<user>();

    private byte reliableChannel;
    private int MAX_USER = 100;

    private int PORT = 26000;
    private int WEB_PORT = 26001;

    private int hostId;
    private int webHostId;

    private bool isStarted = false;
    private const int BYTE_SIZE = 1024;
    private byte error;
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

        hostId = NetworkTransport.AddHost(topo, PORT, null);
        webHostId = NetworkTransport.AddWebsocketHost(topo, WEB_PORT, null);

        Debug.Log(string.Format("Opening connection on port {0} and webport {1}", PORT, WEB_PORT));

        isStarted = true;
    }

    public void ShutDown()
    {
        NetworkTransport.Shutdown();
        isStarted = false;
        Debug.Log("network shutdown");
    }
    // Update is called once per frame
    public void UpdateMessgaePump()
    {
        if (!isStarted)
        {
            return;
        }
        int recHostId; //web client or standalone
        //standalone 0
        //webclient 1
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
                user newUser = new user();
                newUser.channelId = channelId;
                newUser.connectionId = connectionId;
                newUser.recHostId = recHostId;
                users.Add(newUser);
                Debug.Log(string.Format("User {0} has connected through {1} !", connectionId, recHostId));
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected!", connectionId));
                foreach(user userObj in users)
                {
                    if (userObj.channelId==channelId && userObj.recHostId==recHostId && userObj.connectionId == connectionId)
                    {
                        users.Remove(userObj);
                        break;
                    }
                }
                break;

            case NetworkEventType.BroadcastEvent:
                Debug.Log("Broadcast event happened");
                break;

            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMsg msg = (NetMsg)formatter.Deserialize(ms);
                sendClient(msg);//发送消息到所有已经连接的用户
                OnData(connectionId, channelId, recHostId, msg);
                break;
            default:
                break;
        }
    }

    #region OnData
    private void OnData(int cnnId,int channelId,int recHostId,NetMsg msg)
    {
        Debug.Log("Received a message of type " + msg.OP);
        switch (msg.OP)
        {
            case NetOP.None:
                break;
            case NetOP.CreateAccount:
                CreateAccount(cnnId, channelId, recHostId, (CreateAccount)msg);
                break;
            case NetOP.company:
                break;
            case NetOP.block:
                break;
        }
    }
    #endregion
    private void CreateAccount(int cnnId,int channelId,int recHostId,CreateAccount account)
    {
        Debug.Log(string.Format("Account: username:{0}, password: {1}, email: {2}", account.userName, account.passWd, account.email));

    }

    public void sendClient(NetMsg msg)
    {
        foreach(user userObj in users)
        {
            byte[] buffer = new byte[BYTE_SIZE];

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(buffer);
            formatter.Serialize(ms, msg);
            
            NetworkTransport.Send(hostId, userObj.connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
            Debug.Log(string.Format("connectionId: {0} error {1}", userObj.connectionId, error));
        }
    }
}


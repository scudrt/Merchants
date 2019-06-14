using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
public class Client : MonoBehaviour
{
    public  Contract curContract;
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
        /*CreateAccount account = new CreateAccount();
        account.userName = "MilesTin";
        account.passWd = "12345678";
        account.email = "115452@qq.com";
        account.OP = NetOP.CreateAccount;
        SendServer(account);*/
        CompanyInfo companyInfoObj = new CompanyInfo();
        
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
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMsg msg = (NetMsg)formatter.Deserialize(ms);
                OnData(connectionId, channelId, recHostId, msg);
                break;
            default:
                break;
        }
    }

    #region send
    public void SendServer(NetMsg msg)
    {
        byte[] buffer = new byte[BYTE_SIZE];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);
        
        NetworkTransport.Send(hostId, connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
        
    }
    #endregion
    private void OnData(int cnnId, int channelId, int recHostId, NetMsg msg)
    {
        Debug.Log("Received a message of type " + msg.OP);
        switch (msg.OP)
        {
            case NetOP.None:
                break;
            case NetOP.CreateAccount:
                break;
            case NetOP.company:
                CompanyInfo company = (CompanyInfo)msg;
                company.setData();
                break;
            case NetOP.block:
                BlockInfo blockInfo = (BlockInfo)msg;
                blockInfo.SetData();
                break;
            case NetOP.contract:
                Debug.Log("接收到合同");

                ContractInfo contract = (ContractInfo)msg;
                Contract tempContract = contract.contract;
                if (tempContract._targetId != City.currentCompany.id)
                {
                    return;
                }
                curContract = contract.contract;
                
                break;
        }

    }

}

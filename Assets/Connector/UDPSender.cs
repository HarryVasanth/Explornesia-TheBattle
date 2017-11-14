using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;

public class UDPSender : MonoBehaviour
{
    public static string Port = "1112";
    public string Ip = "192.168.1.5";

    public bool Flag, Connected;

    public IPEndPoint RemoteEndPoint;
    public UdpClient Client;

    private static bool _isConnected;

    private bool _first = true;

    private string _iP;
    private int _port;

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //If user pressed Connect and is not connected, then connects, 
        //else if is connected, stops the connection
        if (Connected)
        {
            if (_first)
            {
                Debug.Log("Start UDP");
                _iP = Ip;
                _port = int.Parse(Port);
                Init();
                Flag = true;
                _first = false;
            }
        }
        else
        {
            if (Flag)
            {
                if (!_first)
                {
                    Flag = false;
                    _first = true;
                    Client.Close();
                    Debug.Log("Stop UDP");
                }
            }
        }
    }

    private void Init()
    {
        RemoteEndPoint = new IPEndPoint(IPAddress.Parse(_iP), _port);
        Client = new UdpClient();
        _isConnected = true;
        Debug.Log("Connected");
    }
    
    public void SendStringMessage(string message)
    {
        if (_isConnected)
        {
            try
            {
                if (message != "")
                {
                    // UTF8 encoding to binary format.
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    
                    // Send the message to the remote client.
                    Client.Send(data, data.Length, RemoteEndPoint);
                    Connected = false;
                }
            }

            catch (Exception err)
            {
                Debug.Log(err.ToString());
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        if (Client != null)
        {
            _isConnected = false;
            Client.Close();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : SingleTon<Server>
{
    // 用于保存已经建立好连接的客户端
    // private List<Socket> m_ListClient = new List<Socket>();

    //简化为一个客户端
    private Net m_Client = null;

    public void Receive(NetCmd cmd)
    {

    }

    public void Send(NetCmd cmd)
    {
        Net.instance.Receive(cmd);
    }

    public void Connected(Net client)
    {
        //接受客户请求，保存客户的socket
        m_Client = client;
    }
}

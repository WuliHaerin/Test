using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : SingleTon<Server>
{
    // ���ڱ����Ѿ����������ӵĿͻ���
    // private List<Socket> m_ListClient = new List<Socket>();

    //��Ϊһ���ͻ���
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
        //���ܿͻ����󣬱���ͻ���socket
        m_Client = client;
    }
}

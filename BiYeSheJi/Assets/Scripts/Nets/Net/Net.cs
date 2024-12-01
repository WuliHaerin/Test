using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Net : SingleTon<Net>
{
    private Server m_Server;
    private Server server
    {
        get
        {
            if (m_Server == null)
            {
                m_Server = Server.instance;
            }
            return m_Server;

        }
    }

    private CmdMap m_CmpMap = new CmdMap();

    public void Connect(Action successAction, Action failAction)
    {
        if(true)
        {
            if(successAction!=null)
            {
                successAction();
            }
            server.Connected(this);
        }
        else
        {
            if(failAction!=null)
            {
                failAction();
            }
        }
    }

    public void Send(NetCmd cmd)
    {
        // 走socket去发送网络消息给服务端
        server.Receive(cmd);
    }

    public void Receive(NetCmd cmdData)
    {
        Command cmd = m_CmpMap.GetCommand(cmdData.id);
        if(cmd!=null)
        {
            cmd.Execute(cmdData);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdProcessor 
{
    public static int m_ServerID = 1;
    public static int GetServerID()
    {
        return m_ServerID++;
    }

    //��������½�߼�����
    public static void OnLogin(NetCmd cmdData)
    {
        AccountInfoCmd accountInfo = cmdData as AccountInfoCmd;
        string account = accountInfo.account;
        string password = accountInfo.password;

        // �������ݿ⣬�ж��˺ź������Ƿ�Ϸ�
        // ����Ϸ�����������û���Ϣ���͸��ͻ���
        // ������Ϸ������ش�����Ϣ
        if(true)
        {
            // �ռ�����,UserData
            UserDataCmd userDataCmd = new UserDataCmd();
            userDataCmd.id = 10002;
            userDataCmd.serverID = 1;
            userDataCmd.nickName = "ϡ��";

            Server.instance.Send(userDataCmd);

        }
        else
        {
            // ��֯������Ϣ�������͸��ͻ���
            // Server.instance.Send(errInfo);
        }
    }
}

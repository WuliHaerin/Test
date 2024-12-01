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

    //服务器登陆逻辑处理
    public static void OnLogin(NetCmd cmdData)
    {
        AccountInfoCmd accountInfo = cmdData as AccountInfoCmd;
        string account = accountInfo.account;
        string password = accountInfo.password;

        // 查找数据库，判断账号和密码是否合法
        // 如果合法，将更多的用户信息发送给客户端
        // 如果不合法，返回错误信息
        if(true)
        {
            // 收集数据,UserData
            UserDataCmd userDataCmd = new UserDataCmd();
            userDataCmd.id = 10002;
            userDataCmd.serverID = 1;
            userDataCmd.nickName = "稀土";

            Server.instance.Send(userDataCmd);

        }
        else
        {
            // 组织错误信息，并发送给客户端
            // Server.instance.Send(errInfo);
        }
    }
}

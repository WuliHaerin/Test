using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMap : MonoBehaviour
{
    /// <summary>
    /// 将消息ID和命令进行绑定
    /// </summary>
    private Dictionary<int, Command> cmdIDToCommand = new Dictionary<int, Command>()
    {

    };

    /// <summary>
    /// 根据消息ID获取命令
    /// </summary>
    /// <param name="cmdID">消息ID</param>
    /// <returns>命令</returns>
    public Command GetCommand(int cmdID)
    {
        Command cmd = null;
        if(cmdIDToCommand.TryGetValue(cmdID,out cmd))
        {
            return cmd;
        }
        return null;
    }


}

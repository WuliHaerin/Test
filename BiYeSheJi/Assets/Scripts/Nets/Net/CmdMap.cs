using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMap : MonoBehaviour
{
    /// <summary>
    /// ����ϢID��������а�
    /// </summary>
    private Dictionary<int, Command> cmdIDToCommand = new Dictionary<int, Command>()
    {

    };

    /// <summary>
    /// ������ϢID��ȡ����
    /// </summary>
    /// <param name="cmdID">��ϢID</param>
    /// <returns>����</returns>
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

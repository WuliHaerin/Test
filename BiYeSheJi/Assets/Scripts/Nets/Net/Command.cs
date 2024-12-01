using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有命令处理逻辑的基类
public abstract class Command 
{
    public abstract void Execute(NetCmd cmdData);
}

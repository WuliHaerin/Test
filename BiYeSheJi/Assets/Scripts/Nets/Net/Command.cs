using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����������߼��Ļ���
public abstract class Command 
{
    public abstract void Execute(NetCmd cmdData);
}

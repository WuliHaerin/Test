using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCmd
{
    public int id;
}

public class AccountInfoCmd : NetCmd
{
    public string account;
    public string password;
}

public class UserDataCmd : NetCmd
{
    public int serverID;
    public string nickName;
}

public class RoleSelectCmd : NetCmd
{
    public int roleID;
}

public class User
{
    public int serveID;
    public int roleID;
}

public class EnterMapCmd : NetCmd
{
    public int mapID;
    public List<User> userList = new List<User>();
}

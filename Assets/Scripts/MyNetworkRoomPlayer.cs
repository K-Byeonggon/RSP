using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkRoomPlayer : NetworkRoomPlayer
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Room player started on client");
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Room player started on server");
    }

    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();
        Debug.Log("Client entered room");
    }

    public override void OnClientExitRoom()
    {
        base.OnClientExitRoom();
        Debug.Log("Client exited room");
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("Stop Client");
    }
}

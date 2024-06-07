using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkRoomManager : NetworkRoomManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        maxConnections = 2; // 최대 2명
    }

    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        // 현재 룸에 연결된 플레이어 수가 최대 인원을 넘는지 확인
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        base.OnRoomServerConnect(conn);
    }
}

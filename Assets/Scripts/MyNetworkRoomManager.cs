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

        Debug.Log("OnRoomServerConnect");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        //플레이어가 룸입장 성공하면 플레이어 등록
        Debug.Log("OnServerAddPlayer");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnServerAddPlayer_RegisterPlayer(conn);
        }
        else
        {
            Debug.LogError("GameManager.Instance is null");
        }
    }

    public override void OnRoomServerDisconnect(NetworkConnectionToClient conn)
    {
        //클라가 연결 끊기면 플레이어 등록 해제
        
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnRoomServerDisconnect_UnRegisterPlayer(conn);
        }
        else
        {
            Debug.LogError("GameManager.Instance is null");
        }

        base.OnRoomServerDisconnect(conn);
    }
}

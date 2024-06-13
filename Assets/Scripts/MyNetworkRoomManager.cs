using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkRoomManager : NetworkRoomManager
{
    [SerializeField] GameObject Prefab_GameManager;

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

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        base.OnRoomServerSceneChanged(sceneName);

        //Debug.Log("sceneName: " + sceneName); //scene이름 확인용

        if(sceneName == "Assets/Scenes/GameScene.unity")
        {
            //OnRoomServerSceneChanged는 서버에서만 실행된다.
            //서버에서 생성한 GameManager를 NetworkServer.Spawn을 통해 클라에 동기화 해주어야 한다.
            //NetworkRoomManager의 Registered Spawnable Prefabs에 GameManager프리펩을 추가하는 것을 잊지말자.

            GameObject gameManager = Instantiate(Prefab_GameManager);
            NetworkServer.Spawn(gameManager);
        }
        else
            Debug.Log("OnRoomServersceneChanged: GameScene 아님.");
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        Debug.Log($"(conn.identity == null) = {conn.identity == null}");

        Debug.Log("OnServerReady: 플레이어 GameManager에 등록 시도");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnServerReady_RegisterPlayer(conn);
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

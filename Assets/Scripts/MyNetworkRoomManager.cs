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
        maxConnections = 2; // �ִ� 2��
    }

    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        // ���� �뿡 ����� �÷��̾� ���� �ִ� �ο��� �Ѵ��� Ȯ��
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

        //Debug.Log("sceneName: " + sceneName); //scene�̸� Ȯ�ο�

        if(sceneName == "Assets/Scenes/GameScene.unity")
        {
            //OnRoomServerSceneChanged�� ���������� ����ȴ�.
            //�������� ������ GameManager�� NetworkServer.Spawn�� ���� Ŭ�� ����ȭ ���־�� �Ѵ�.
            //NetworkRoomManager�� Registered Spawnable Prefabs�� GameManager�������� �߰��ϴ� ���� ��������.

            GameObject gameManager = Instantiate(Prefab_GameManager);
            NetworkServer.Spawn(gameManager);
        }
        else
            Debug.Log("OnRoomServersceneChanged: GameScene �ƴ�.");
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        Debug.Log($"(conn.identity == null) = {conn.identity == null}");

        Debug.Log("OnServerReady: �÷��̾� GameManager�� ��� �õ�");

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
        //Ŭ�� ���� ����� �÷��̾� ��� ����
        
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

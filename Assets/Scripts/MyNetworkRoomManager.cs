using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkRoomManager : NetworkRoomManager
{

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

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        //�÷��̾ ������ �����ϸ� �÷��̾� ���
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

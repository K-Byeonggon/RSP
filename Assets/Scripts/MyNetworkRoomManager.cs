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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSP_Enums;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public MyPlayer Player_Server;
    public MyPlayer Player_Client;

    public RSP RSP_Server = RSP.Default;
    public RSP RSP_Client = RSP.Default;

    public WinLose WinLose_Server = WinLose.Default;
    public WinLose WinLose_Client = WinLose.Default;
    


    public void OnStartClient_MyPlayer_RegisterPlayer(MyPlayer player)
    {
        
        Debug.Log("this.isServer: " + this.isServer);
        

        if (player.isLocalPlayer)
        {
            Debug.Log("isServer: " + player.isServer);
            Debug.Log("isClient: " + player.isClient);
            Debug.Log("isServerOnly: " + player.isServerOnly);
            Debug.Log("isClientOnly: " + player.isClientOnly);
        }

        //������ ���ӸŴ���
        if (this.isServer)
        {
            if(player.isLocalPlayer) { Debug.Log("�����÷��̾� ����Ϸ�����"); Player_Server = player; }
            else { Debug.Log("Ŭ���÷��̾� ����Ϸ�����"); Player_Client = player; }
        }
        else 
        {
            if (!player.isLocalPlayer) { Debug.Log("�����÷��̾� ����Ϸ�����"); Player_Server = player; }
            else { Debug.Log("Ŭ���÷��̾� ����Ϸ�����"); Player_Client = player; }
        }

        //if (player.isServer) { Debug.Log("�����÷��̾� ����Ϸ�����"); Player_Server = player; }
        //else if (!player.isServer) { Debug.Log("Ŭ���÷��̾� ����Ϸ�����"); Player_Client = player; }

        //�̷��� ���������� player.isLocal && player.isServer �̸� ������, !player.isLocal && !player.isServer �̸� Ŭ��,
        //      Ŭ�󿡼��� !player.isLocal && player.isServer �̸� ������, player.isLocal && !player.isServer�� Ŭ��.
        //������ ���ӸŴ����� ��� �ֳĿ� ���� ������ �޶���. �׷��� ������ ������.

    }


    public void CheckChoices()
    {
        if (Player_Server == null ) { Debug.LogError("���� �÷��̾� ��� �ȵ�!"); return; }
        if (Player_Client == null ) { Debug.LogError("Ŭ�� �÷��̾� ��� �ȵ�!"); return; }

        Debug.Log("�Ƹ� ���������� ����Ǵ� ��");
        if (RSP_Server != RSP.Default &&
            RSP_Client != RSP.Default)
        {
            Debug.Log("�� �÷��̾��� ������ �޴µ� �����ߴ�.");
            DetermineWinLose(RSP_Server, RSP_Client, 
                out WinLose_Server, out WinLose_Client);

            Player_Server.RpcSendResult(WinLose_Server);
            Player_Client.RpcSendResult(WinLose_Client);
        }
    }

    public void DetermineWinLose(RSP p1_RSP, RSP p2_RSP, out WinLose p1_WinLose, out WinLose p2_WinLose)
    {
        if(p1_RSP == p2_RSP)
        {
            p1_WinLose = WinLose.DRAW;
            p2_WinLose = WinLose.DRAW;
        }
        else if((p1_RSP == RSP.ROCK && p2_RSP == RSP.SCISSORS)
            || (p1_RSP == RSP.SCISSORS && p2_RSP == RSP.PAPER
            || (p1_RSP == RSP.PAPER && p2_RSP == RSP.ROCK)))
        {
            p1_WinLose = WinLose.WIN;
            p2_WinLose = WinLose.LOSE;
        }
        else
        {
            p1_WinLose = WinLose.LOSE;
            p2_WinLose = WinLose.WIN;
        }
    }
}

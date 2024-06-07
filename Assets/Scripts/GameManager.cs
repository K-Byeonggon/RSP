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

        //서버의 게임매니저
        if (this.isServer)
        {
            if(player.isLocalPlayer) { Debug.Log("서버플레이어 등록하려고함"); Player_Server = player; }
            else { Debug.Log("클라플레이어 등록하려고함"); Player_Client = player; }
        }
        else 
        {
            if (!player.isLocalPlayer) { Debug.Log("서버플레이어 등록하려고함"); Player_Server = player; }
            else { Debug.Log("클라플레이어 등록하려고함"); Player_Client = player; }
        }

        //if (player.isServer) { Debug.Log("서버플레이어 등록하려고함"); Player_Server = player; }
        //else if (!player.isServer) { Debug.Log("클라플레이어 등록하려고함"); Player_Client = player; }

        //이러면 서버에서는 player.isLocal && player.isServer 이면 서버에, !player.isLocal && !player.isServer 이면 클라에,
        //      클라에서는 !player.isLocal && player.isServer 이면 서버에, player.isLocal && !player.isServer면 클라에.
        //완전히 게임매니저가 어디 있냐에 따라 동작이 달라짐. 그래서 문제가 생겼음.

    }


    public void CheckChoices()
    {
        if (Player_Server == null ) { Debug.LogError("서버 플레이어 등록 안됨!"); return; }
        if (Player_Client == null ) { Debug.LogError("클라 플레이어 등록 안됨!"); return; }

        Debug.Log("아마 서버에서만 실행되는 것");
        if (RSP_Server != RSP.Default &&
            RSP_Client != RSP.Default)
        {
            Debug.Log("두 플레이어의 정보를 받는데 성공했다.");
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

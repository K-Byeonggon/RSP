using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSP_Enums;
using Mirror;

public class GameManager : NetworkBehaviour
{
    //�������� �÷��̾���� ����Ϸ��� GameManager�� OnlineScene�� �����ϰ� Singleton�� �ʿ䰡 ���� �� ����.
    public static GameManager Instance { get; private set; }

    public MyPlayer Player_You;
    public MyPlayer Player_Oppo;

    public RSP RSP_You = RSP.Default;
    public RSP RSP_Oppo = RSP.Default;

    public WinLose WinLose_You = WinLose.Default;
    public WinLose WinLose_Oppo = WinLose.Default;

    public List<MyPlayer> players = new List<MyPlayer>(2);

    /*
    GameManager�� �������� ���� ������ �����͸� ó���ϰ�,
    Ŭ�󿡼��� UI�� ������Ʈ�ϰ� ����� ǥ���Ѵ�.
     */


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnRoomServerAddPlayer_RegisterPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("OnRoomServerAddPlayer_RegisterPlayer");
        var player = conn.identity.GetComponent<MyPlayer>();

        if (!players.Contains(player))
        {
            players.Add(player);
            Debug.Log("Player registered: " + player.netId);
        }
    }

    public void OnRoomServerDisconnect_UnRegisterPlayer(NetworkConnectionToClient conn)
    {
        var player = conn.identity.GetComponent<MyPlayer>();

        if (players.Contains(player))
        {
            players.Remove(player);
            Debug.Log("Player unregistered: " + player.netId);
        }
    }


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

        //���� ������ ���� �ְ�, ������ �ƴ� �� Ŭ�� �濡 ���� ������ �����Ѵ�.
        //�׷��� �÷��̾� ����� Player1, Player2 �̷��� ����ؾ߰ڴ�.


    }


    public void CheckChoices()
    {
        if (Player_You == null ) { Debug.LogError("�÷��̾�(��) ��� �ȵ�!"); return; }
        if (Player_Oppo == null ) { Debug.LogError("�÷��̾�(���) ��� �ȵ�!"); return; }

        Debug.Log("�Ƹ� ���������� ����Ǵ� ��");
        if (RSP_You != RSP.Default &&
            RSP_Oppo != RSP.Default)
        {
            Debug.Log("�� �÷��̾��� ������ �޴µ� �����ߴ�.");
            DetermineWinLose(RSP_You, RSP_Oppo, out WinLose_You, out WinLose_Oppo);

            Player_You.RpcSendResult(WinLose_You);
            Player_Oppo.RpcSendResult(WinLose_Oppo);
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

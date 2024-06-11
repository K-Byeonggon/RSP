using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using RSP_Enums;

public class MyPlayer : NetworkBehaviour
{
    public RSP _currentRSP = RSP.Default;
    public WinLose _currentWinLose = WinLose.Default;

    public GameManager _gameManager;
    public RockScissorsPaperUI _rockScissorsPaperUI;

    [Header("Sprites 0:Rock 1:Scissors 2:Paper")]
    [SerializeField] Sprite[] Sprites_RSP = new Sprite[3];

    public override void OnStartClient()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null) { Debug.LogError("���ӸŴ��� ����"); return; }
        _gameManager.OnStartClient_MyPlayer_RegisterPlayer(this);

        if (isLocalPlayer)
        {
            _rockScissorsPaperUI = FindObjectOfType<RockScissorsPaperUI>();
            if (_rockScissorsPaperUI == null) { Debug.LogError("����������UI ����"); return; }
            _rockScissorsPaperUI.Btn_Rock.onClick.AddListener(
                () => {  _currentRSP = RSP.ROCK; Debug.Log("��ư �Է��� ���� ����?"); SendMove(); });
            _rockScissorsPaperUI.Btn_Scissors.onClick.AddListener(
                () => {  _currentRSP = RSP.SCISSORS; Debug.Log("��ư �Է��� ���� ����?"); SendMove(); });
            _rockScissorsPaperUI.Btn_Paper.onClick.AddListener(
                () => { _currentRSP = RSP.PAPER; Debug.Log("��ư �Է��� �� ����?"); SendMove(); });
        }
    }


    public void SendMove()
    {
        Debug.Log("SendMove �۵�����?");
        if (isLocalPlayer)
        {
            Debug.Log("�����÷��̾ Cmd ȣ����");
            CmdSendMoveToGameManager(_currentRSP);
        }
    }

    [Command]
    public void CmdSendMoveToGameManager(RSP playerRSP)
    {
        Debug.Log("Cmd���� ���� ����?");
        
        _gameManager.RSP_You = playerRSP;

        _gameManager.CheckChoices();
    }

    [ClientRpc]
    public void RpcSendResult(WinLose winLose)
    {
        Debug.Log("isServer:" + isServer + " �̰��� �Ƹ� ��� Ŭ�󿡼�");

        _currentWinLose = winLose;

        

        if (isLocalPlayer)
        {
            if(winLose == WinLose.WIN)
            {
                _rockScissorsPaperUI.Txt_WinLose.text = "You Win!";
            }
            else if(winLose == WinLose.DRAW)
            {
                _rockScissorsPaperUI.Txt_WinLose.text = "DRAW";
            }
            else if(winLose == WinLose.LOSE)
            {
                _rockScissorsPaperUI.Txt_WinLose.text = "You Lose..";
            }
            else
            {
                _rockScissorsPaperUI.Txt_WinLose.text = "???";
            }

            _rockScissorsPaperUI.Img_YourRSP.sprite = Sprites_RSP[(int)this._currentRSP];

            if (this._currentWinLose == WinLose.WIN)
            {
                switch (this._currentRSP)
                {
                    case (RSP.ROCK):
                        _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[1];
                        break;
                    case (RSP.SCISSORS):
                        _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[2];
                        break;
                    case (RSP.PAPER):
                        _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[0];
                        break;
                }
            }
            else if (this._currentWinLose == WinLose.DRAW)
            {
                _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[(int)this._currentRSP];
            }
            else
            {
                switch (this._currentRSP)
                {
                    case (RSP.ROCK):
                        _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[2];
                        break;
                    case (RSP.SCISSORS):
                        _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[0];
                        break;
                    case (RSP.PAPER):
                        _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[1];
                        break;
                }
            }

            
        }

    }


}

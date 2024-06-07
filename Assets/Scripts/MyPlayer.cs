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
        if (_gameManager == null) { Debug.LogError("게임매니저 없음"); return; }
        _gameManager.OnStartClient_MyPlayer_RegisterPlayer(this);

        if (isLocalPlayer)
        {
            _rockScissorsPaperUI = FindObjectOfType<RockScissorsPaperUI>();
            if (_rockScissorsPaperUI == null) { Debug.LogError("가위바위보UI 없음"); return; }
            _rockScissorsPaperUI.Btn_Rock.onClick.AddListener(
                () => {  _currentRSP = RSP.ROCK; Debug.Log("버튼 입력은 바위 되지?"); SendMove(); });
            _rockScissorsPaperUI.Btn_Scissors.onClick.AddListener(
                () => {  _currentRSP = RSP.SCISSORS; Debug.Log("버튼 입력은 가위 되지?"); SendMove(); });
            _rockScissorsPaperUI.Btn_Paper.onClick.AddListener(
                () => { _currentRSP = RSP.PAPER; Debug.Log("버튼 입력은 보 되지?"); SendMove(); });
        }
    }


    public void SendMove()
    {
        Debug.Log("SendMove 작동하지?");
        if (isLocalPlayer)
        {
            Debug.Log("로컬플레이어가 Cmd 호출함");
            CmdSendMoveToGameManager(_currentRSP);
        }
    }

    [Command]
    public void CmdSendMoveToGameManager(RSP playerRSP)
    {
        Debug.Log("Cmd까지 오긴 오지?");
        
        if(isServer && isClient) { _gameManager.RSP_Server = playerRSP; }
        else if (!isServer && isClient){ _gameManager.RSP_Client = playerRSP; }

        _gameManager.CheckChoices();
    }

    [ClientRpc]
    public void RpcSendResult(WinLose result)
    {
        Debug.Log("isServer:" + isServer + " 이것은 아마 모든 클라에서");

        if (_rockScissorsPaperUI == null)
        {
            Debug.LogError("RpcSendResult: _rockScissorsPaperUI is null.");
            return;
        }

        _rockScissorsPaperUI.Img_YourRSP.sprite = Sprites_RSP[(int)this._currentRSP];

        if(result == WinLose.WIN)
        {
            switch (this._currentRSP) 
            {
                case(RSP.ROCK):
                    _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[1];
                    break;
                case(RSP.SCISSORS):
                    _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[2];
                    break;
                case(RSP.PAPER):
                    _rockScissorsPaperUI.Img_OppoRSP.sprite = Sprites_RSP[0];
                    break;
            }
        }
        else if(result == WinLose.DRAW)
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

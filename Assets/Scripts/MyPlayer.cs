using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using RSP_Enums;

public class MyPlayer : NetworkBehaviour
{
    public RSP _currentRSP = RSP.Default;
    public WinLose _currentState = WinLose.Default;

    public GameManager _gameManager;
    public RockScissorsPaperUI _rockScissorsPaperUI;

    public override void OnStartClient()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null) { Debug.LogError("게임매니저 없음"); return; }
        _gameManager.OnStartClient_MyPlayer_RegisterPlayer(this);

        _rockScissorsPaperUI =FindObjectOfType<RockScissorsPaperUI>();
        if (_rockScissorsPaperUI == null) { Debug.LogError("가위바위보UI 없음"); return; }
        _rockScissorsPaperUI.Btn_Rock.onClick.AddListener(() => _currentRSP = RSP.ROCK);
        _rockScissorsPaperUI.Btn_Scissors.onClick.AddListener(() => _currentRSP = RSP.SCISSORS);
        _rockScissorsPaperUI.Btn_Paper.onClick.AddListener(() => _currentRSP = RSP.PAPER);
    }

    [Command]
    public void CmdMakeChoice()
    {
        _gameManager.CheckChoices();
    }

    [ClientRpc]
    public void RpcSendResult()
    {
        _rockScissorsPaperUI.Img_YourRSP.sprite = _rockScissorsPaperUI.Sprites_RSP[(int)this._currentRSP];
        //상대가 낸 것도 띄워줘야 하니까 이런 방식은 문제가 있을 수 있을 것 같다.
    
    }


}

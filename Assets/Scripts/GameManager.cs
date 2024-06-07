using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSP_Enums;

public class GameManager : MonoBehaviour
{
    private MyPlayer[] _players = new MyPlayer[2];
    private int _playerCount = 0;

    public void OnStartClient_MyPlayer_RegisterPlayer(MyPlayer player)
    {
        if(_playerCount < 2)
        {
            _players[_playerCount] = player;
            _playerCount++;
        }
    }

    public void CheckChoices()
    {
        if (_players[0]._currentRSP != RSP.Default &&
            _players[1]._currentRSP != RSP.Default)
        {
            DetermineWinLose(_players[0]._currentRSP, _players[1]._currentRSP, 
                out _players[0]._currentState, out _players[1]._currentState);
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

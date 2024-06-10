using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScissorsPaperUI : MonoBehaviour
{
    [Header("Panel_WinLose")]
    [SerializeField] public Image Img_YourRSP;
    [SerializeField] public Image Img_OppoRSP;
    [SerializeField] public Text Txt_WinLose;
    [SerializeField] Text Txt_Countdown;

    [Header("Panel_RSP")]
    [SerializeField] public Button Btn_Rock;
    [SerializeField] public Button Btn_Scissors;
    [SerializeField] public Button Btn_Paper;
    [SerializeField] Text Txt_Waiting;

    [Header("Sprites: 0.Rock 1.Scissors 2.Paper")]
    [SerializeField] public Sprite[] Sprites_RSP = new Sprite[3];
    [SerializeField] public Sprite Sprites_Empty;

}

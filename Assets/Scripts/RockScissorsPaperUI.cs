using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScissorsPaperUI : MonoBehaviour
{
    [Header("Panel_WinLose")]
    [SerializeField] Image Img_YourRSP;
    [SerializeField] Image Img_OppoRSP;
    [SerializeField] Text Txt_WinLose;
    [SerializeField] Text Txt_Countdown;

    [Header("Panel_RSP")]
    [SerializeField] Button Btn_Rock;
    [SerializeField] Button Btn_Scissors;
    [SerializeField] Button Btn_Paper;
    [SerializeField] Text Txt_Waiting;

    [Header("Sprites")]
    [SerializeField] Sprite Sprite_Rock;
    [SerializeField] Sprite Sprite_Scissors;
    [SerializeField] Sprite Sprite_Paper;

}

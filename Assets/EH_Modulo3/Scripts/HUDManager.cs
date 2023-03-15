using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Transition")]
    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private string gameOver = "GAME OVER";
    [SerializeField] private string winGame = "YOU WIN";

    public void EndGame(bool isWin)
    {
        endGameText.text = isWin ? winGame : gameOver;

        fadeGroup.DOFade(1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private EnemyController enemy;

    [Header("HUD")]
    [SerializeField] private HUDManager hud;

    [Header("Musics")]
    [SerializeField] private AudioSource gameplayMusic;
    [SerializeField] private AudioSource gameOverMusic;
    [SerializeField] private AudioSource gameWinMusic;

    private void Awake()
    {
        enemy.OnDie += Enemy_OnDie;
        player.OnDie += Player_OnDie;
    }

    private void Enemy_OnDie()
    {
        GameEnd();

        gameWinMusic.Play();
        gameWinMusic.DOFade(1, 1);

        hud.EndGame(true);
    }

    private void Player_OnDie()
    {
        GameEnd();

        gameOverMusic.Play();
        gameOverMusic.DOFade(1, 1);

        hud.EndGame(false);
    }

    private void GameEnd()
    {
        player.IsInvincible = true;
        player.CanShoot = false;
        player.CanDash = false;

        player.enabled = false;
        enemy.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameplayMusic.DOFade(0, 1);
    }

    bool isLoading;
    public void Retry()
    {
        if (isLoading) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        if (isLoading) return;

        Application.Quit();
    }
}

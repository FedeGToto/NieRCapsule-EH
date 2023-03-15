using SCPE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class PlayerDash : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerMovement playerMovement;

    [Header("Dash values")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private AudioClip dashSound;

    [Header("Post Processing")]
    [SerializeField] private Volume postProcessingVolume;
    private float dashTimer;

    private RadialBlur radialBlur;
    private SpeedLines speedLines;

    private bool isDashing;



    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerMovement = playerManager.GetMovement();

        postProcessingVolume.profile.TryGet<RadialBlur>(out radialBlur);
        postProcessingVolume.profile.TryGet<SpeedLines>(out speedLines);
    }

    public void UpdateDash()
    {
        if (InputManager.DASH && playerManager.CanMove && !isDashing)
        {
            if (Time.realtimeSinceStartup >= dashTimer)
            {
                playerManager.IsInvincible = true;
                StartCoroutine(Dash());

                dashTimer = Time.realtimeSinceStartup + dashCooldown;
            }
        }

    }

    IEnumerator Dash()
    {
        float startTime = Time.realtimeSinceStartup;
        isDashing = true;
        float radialBlurAmout = 0;

        InputManager.ENABLED = false;

        AudioSource.PlayClipAtPoint(dashSound, Camera.main.transform.position, 10f);

        DOTween.To(() => radialBlurAmout, x => radialBlurAmout = x, 1, dashDuration / 4).OnUpdate(() =>
        {
            radialBlur.amount.value = radialBlurAmout;
            speedLines.intensity.value = radialBlurAmout;
        });

        Vector3 direction = playerMovement.DirectionToMove;

        while (Time.realtimeSinceStartup < startTime + dashDuration)
        {

            if (direction.magnitude <= 0)
                Debug.Log("Magnitude is 0");

            playerMovement.GetCharacterController().Move(dashSpeed * Time.deltaTime * playerMovement.DirectionToMove);

            yield return null;
        }

        playerManager.IsInvincible = false;
        InputManager.ENABLED = true;
        isDashing = false;

        DOTween.To(() => radialBlurAmout, x => radialBlurAmout = x, 0, dashDuration / 4).OnUpdate(() =>
        {
            radialBlur.amount.value = radialBlurAmout;
            speedLines.intensity.value = radialBlurAmout;
        });
    }

}
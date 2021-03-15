using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour {
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip attackClip;

    private void Awake() {
        playerMovement.StartedDash += OnDashStart;
        playerMovement.StartedAttacking += OnAttackStart;
    }

    private void OnAttackStart() {
        audioSource.clip = attackClip;
        audioSource.Play();
    }

    private void OnDashStart() {
        audioSource.clip = dashClip;
        audioSource.Play();
    }
}

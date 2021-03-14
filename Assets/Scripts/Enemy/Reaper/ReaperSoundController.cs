using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSoundController : MonoBehaviour {
    [SerializeField] private Reaper reaper;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip attackClip;
    
    private void Awake() {
        reaper.StartedAttacking += OnAttackStart;
    }

    private void OnAttackStart() {
        audioSource.clip = attackClip;
        audioSource.Play();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageSystem damageSource = other.GetComponent<DamageSystem>();

        if(damageSource != null){
            TakeDamage(damageSource.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageSource.Hit();
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake){
            cameraShake.Play();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(!isPlayer){
            scoreKeeper.ModifyScore(score);
        }
        Destroy(gameObject);
    }

    void PlayHitEffect(){
        if(hitEffect !=null){
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public int GetHealth(){
        return health;
    }
}

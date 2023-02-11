using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    [HideInInspector] public bool isFiring;
    [Header("General")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFireRate = 0.1f;

    Coroutine firingCoroutine;
    GameObject player;
    AudioPlayer audioPlayer;
    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }


    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }

    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();

            if (rb2d != null)
            {
                /*if(useAI){
                    player = GameObject.FindWithTag("Player");
                Vector3 playerPos = player.transform.position;
                instance.transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: playerPos);
                rb2d.velocity = playerPos * projectileSpeed * 0.5f;
                }else{*/
                    rb2d.velocity = transform.up * projectileSpeed;
                //}
                   
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = UnityEngine.Random.Range(baseFireRate - firingRateVariance,
                                                            baseFireRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile,
                                               minimumFireRate,
                                               float.MaxValue);

            audioPlayer.PlayShootingClip();
            
            yield return new WaitForSeconds(timeToNextProjectile);
        }

    }
}

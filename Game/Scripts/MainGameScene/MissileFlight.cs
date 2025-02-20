﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFlight : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 50;
    public float speed = 1f;
    public float maxSpeed = 20f;
    public float maxY;
    public GameObject missleTrail;
    Vector3 temp;
    bool collided;
    BossEnemyMovement bossScript;
    MiddleEnemyMovement middleEnemyScript;

    CoinAndScoreGain casg;

    void Start()
    {
        casg = GameObject.FindWithTag("GameController").GetComponent<CoinAndScoreGain>();
        if (GameObject.FindWithTag("MiddleEnemy") != null) {
            middleEnemyScript = GameObject.FindWithTag("MiddleEnemy").GetComponent<MiddleEnemyMovement>();
        }
        collided = false;
        if (GameObject.FindWithTag("BossEnemy") != null) {
            bossScript = GameObject.FindWithTag("BossEnemy").GetComponent<BossEnemyMovement>();
        }
        SetDamage();
    }

    void SetDamage() {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>() != null) {
            damage += casg.playerData.firstShipLevel;
        }
        if (GameObject.FindWithTag("Player").GetComponent<PlayerControllerSpaceShip2>() != null) {
            damage += casg.playerData.secondShipLevel;
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag("BossEnemy") != null) {
            bossScript = GameObject.FindWithTag("BossEnemy").GetComponent<BossEnemyMovement>();
        }
        if (GameObject.FindWithTag("MiddleEnemy") != null) {
            middleEnemyScript = GameObject.FindWithTag("MiddleEnemy").GetComponent<MiddleEnemyMovement>();
        }
        AccelerateMissle();
        Move();
        CheckFlyingAway();
    }

    void AccelerateMissle()
    {
        if (speed < maxSpeed)
        {
            speed += 0.05f;
        }
    }

    void Move()
    {
        temp = transform.position;
        temp.y += speed * Time.deltaTime;
        if (temp.y > maxY){
            Destroy(gameObject);
        }
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Asteroid" || target.tag == "Enemy" && !collided) {
            collided = true;
            Destroy(gameObject);
        }
        if (target.tag == "BossEnemy" && !collided) {
            
            Destroy(gameObject);
            bossScript.TakeDamage(damage);
            collided = true;
        }
        if (target.tag == "MiddleEnemy" && !collided) {
            
            Destroy(gameObject);
            middleEnemyScript.TakeDamage(damage);
            collided = true;
        }
    
    }

    void CheckFlyingAway() {
        if (transform.position.y > 7f) {
            Destroy(gameObject);
        }
    }
}

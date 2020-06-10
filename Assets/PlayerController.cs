﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidBody;
    public static int collectedAmount;
    public Text collectedText;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > (lastFire + fireDelay)) {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

        if( Input.GetAxisRaw("Dash") != 0) {
            renderer.color = new Color(0f, 0f, 0f, 1f); // Set to opaque black
            rigidBody.velocity = new Vector3(horizontal * (speed * 5), vertical * (speed * 5), 0);
        }
        else {
            renderer.color = new Color(1f, 1f, 1f, 1f); // Set to opaque black
            rigidBody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        }

        collectedText.text = "Items collected: " + collectedAmount;
    }

    void Shoot(float x, float y) {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 移動速度
    float moveSpeed = 5.0f;
    // ジャンプ力
    float jumpForce = 250.0f;

    // Rigidbody
    Rigidbody rb;
    // Animator
    Animator animator;
    // 今の状態
    int state;
    // 少し前の状態
    int prevState;
    // 地面に接触しているか
    bool ground = true;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        // Animatorを取得
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 今の状態を取得
        // stateが0なら待機状態、1なら走っている、2ならジャンプ中
        if (ground)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                state = 1;
            }
            else
            {
                state = 0;
            }
        }
        else
        {
            state = 2;
        }

        // 今の状態が少し前の状態から変化していたらアニメーションを変更
        if (prevState != state)
        {
            switch (state)
            {
                case 0:
                    animator.SetBool("Idling", true);
                    animator.SetBool("Running", false);
                    animator.SetBool("Jumping", false);
                    break;
                case 1:
                    animator.SetBool("Idling", false);
                    animator.SetBool("Running", true);
                    animator.SetBool("Jumping", false);
                    break;
                case 2:
                    animator.SetBool("Idling", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Jumping", true);
                    break;
            }

            // 少し前の状態を更新
            prevState = state;
        }

        // 入力されたキーに合わせて動かす
        if (ground && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce);
            ground = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position + -transform.right * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + -transform.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * moveSpeed);
        }
    }

    // ジャンプ後地面に接触したか判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ground" && !ground)
        {
            ground = true;
        }
    }
}

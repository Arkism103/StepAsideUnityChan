﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{

    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float velocityZ = 16f;
    private float velocityX = 10f;
    private float velocityY = 10f;
    private float movableRange = 3.4f;
    private float coefficient = 0.95f;
    private bool isEnd = false;
    private GameObject stateText;
    private GameObject scoreText;
    private int score = 0;
    private bool isLButtunDown = false;
    private bool isRButtunDown = false;
    private bool isJButtunDown = false;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat ("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();

        this.stateText = GameObject.Find("GameResultText");

        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        float inputVelocityX = 0;
        float inputVelocityY = 0;

        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtunDown) && -this.movableRange < this.transform.
            position.x)
        {
            inputVelocityX = -this.velocityX;
        }

        else if((Input.GetKey (KeyCode.RightArrow) || this.isRButtunDown) && this.transform.position.x < this.movableRange)
        {
            inputVelocityX = this.velocityX;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtunDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            inputVelocityY = this.velocityY;
        }
        else
        {
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }


        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "Game Over";
        }

        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "Clear!!";
        }

        if(other.gameObject.tag == "CoinTag")
        {
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
        }
    }
    public void GetMyJumpButtonDown()
    {
        this.isJButtunDown = true;
    }
    public void GetmyJumpButtonUp()
    {
        this.isJButtunDown = false;
    }
    public void GetmyLeftButtonDown()
    {
        this.isLButtunDown = true;
    }
    public void GetmyLeftButtonUp()
    {
        this.isLButtunDown = false;
    }
    public void GetmyRightButtonDown()
    {
        this.isRButtunDown = true;
    }
    public void GetmyRightButtonUp()
    {
        this.isRButtunDown = false;
    }
}

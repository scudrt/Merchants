using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Data Area begin
    bool movable; //not movable in main menu
    float moveSpeed, rollSpeed;
    float rollLimitBack, rollLimitAhead;
    float moveLimitBack, moveLimitAhead, moveLimitSide;
    //Data Area end

    //Test Data Area Begin
    int frameTick;
    bool showFPS;
    //Test Data Area End

    private void __judgeAndMove()
    {
        //Camera movement ONLY for rotation:(45, 0, 0)
        if (!movable)
        {
            return;
        }
        Vector3 pos = this.transform.position;

        //left and right
        float sensity = Input.GetAxis("Horizontal");
        if (sensity != 0.0f)
        {
            if ((sensity > 0 && pos.x <= moveLimitSide) || (sensity < 0 && pos.x >= -moveLimitSide))
            {
                this.transform.Translate(moveSpeed * sensity, 0, 0);
            }
        }

        //forward and backward
        float ySpeed = moveSpeed / Mathf.Sqrt(2), zSpeed = moveSpeed / Mathf.Sqrt(2);
        sensity = Input.GetAxis("Vertical");
        if (sensity != 0.0f)
        {
            if ((sensity > 0 && pos.z <= moveLimitAhead) || (sensity < 0 && pos.z >= moveLimitBack))
            {
                this.transform.Translate(0, ySpeed * sensity, zSpeed * sensity);
            }
        }

        //camera movement: roll in and roll out
        sensity = Input.GetAxis("Mouse ScrollWheel");
        if (sensity != 0)
        {
            if ((sensity < 0 && pos.y <= rollLimitBack) || (sensity > 0 && pos.y >= rollLimitAhead))
            {
                this.transform.Translate(0, 0, rollSpeed * sensity);
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        /********************init start********************/
        movable = true;
        moveSpeed = 0.16f;
        rollSpeed = 4.00f;
        rollLimitAhead = 1.0f;
        rollLimitBack = 16.0f;
        moveLimitAhead = 12.0f;
        moveLimitBack = -20.0f;
        moveLimitSide = 16.0f;

        frameTick = 0;
        showFPS = false;
        /********************init done ********************/

        Debug.Log("Camera Script: Start() Accomplished.");
    }

    // Update is called once per frame
    void Update()
    {
        if (showFPS)
        {
            ++this.frameTick;
            if (this.frameTick % 60 == 0)
            {
                float secondPerFrame = Time.timeSinceLevelLoad / this.frameTick;
                Debug.Log(1.0f / secondPerFrame);
            }
        }
        //Move the camera
        __judgeAndMove();
    }
}

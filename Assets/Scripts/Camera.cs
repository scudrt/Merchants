using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Data Area begin
    bool movable; //not movable in main menu
    float moveSpeed, rollSpeed;
    float rollLimitBack, rollLimitAhead, rotateSpeed;
    float sideLimit;
    //Data Area end

    //Test Data Area Begin
    int frameTick;
    bool showFPS = true;
    //Test Data Area End
    
    void Start()
    {
        /********************init start********************/
        movable = true;
        moveSpeed = 0.24f;
        rollSpeed = 4.00f;
        rollLimitAhead = 1.5f;
        rollLimitBack = 10.0f;
        rotateSpeed = 3.0f;
        sideLimit = 16.0f;

        frameTick = 0;
        showFPS = false;
        /********************init done ********************/

        //Debug.Log("Camera Script: Start() Accomplished.");
    }

    private void __judgeAndMove()
    {
        //Camera movement ONLY for rotation:(45, 0, 0)
        if (!movable)
        {
            return;
        }

        //Get camera's position and rotation
        Vector3 pos = this.transform.position,
                direction = Vector3.zero;
        float rot = this.transform.rotation.eulerAngles.y;

        //left and right
        float sensity = Input.GetAxis("Horizontal");
        if (sensity != 0.0f)
        {
            direction.x = moveSpeed * sensity;
        }

        //forward and backward
        sensity = Input.GetAxis("Vertical");
        if (sensity != 0.0f)
        {
            direction.z = moveSpeed * sensity;
        }
        
        //turn vector to world space
        float tempx = Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z), tempz;
        if (tempx != 0.0f)
        {
            rot = (rot + Mathf.Asin(direction.x / tempx)) * Mathf.Deg2Rad;
        }
        float sin = Mathf.Sin(rot), cos = Mathf.Cos(rot);
        tempx = direction.z * sin + direction.x * cos;
        tempz = direction.z * cos - direction.x * sin;
        //check the border and move
        if (Mathf.Abs(pos.x + tempx) >= sideLimit)
        {
            tempx = 0;
        }
        if (Mathf.Abs(pos.z + tempz) >= sideLimit)
        {
            tempz = 0;
        }
        this.transform.Translate(tempx, 0, tempz, Space.World);

        //camera movement: roll in and roll out
        sensity = Input.GetAxis("Mouse ScrollWheel");
        if (sensity != 0)
        {
            if ((sensity < 0 && pos.y <= rollLimitBack) || (sensity > 0 && pos.y >= rollLimitAhead))
            {
                this.transform.Translate(0, 0, rollSpeed * sensity);
            }
        }

        //camera movement: horizontal rotation, no vertical for now
        if (Input.GetMouseButton(1)) //right click
        {
            sensity = Input.GetAxis("Mouse X");
            this.transform.Rotate(0, sensity * rotateSpeed, 0, Space.World);
            sensity = Input.GetAxis("Mouse Y");
            rot = this.transform.rotation.eulerAngles.x;
            sensity *= -rotateSpeed;
            if ((rot + sensity) >= 15.0f && (rot + sensity)<= 75.0f)
            {
                this.transform.Rotate(sensity, 0, 0);
            }
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float posX = player.position.x;
        float posY = player.position.y;
        if (posX < 56f && posX > -7f)
        {
            if (posY > -14.5f && posY < 16f)
            {
                Vector3 temp = this.transform.position;
                temp.x = posX;
                temp.y = posY;
                this.transform.position = temp;
            }
            else
            {
                Vector3 temp = this.transform.position;
                temp.x = posX;
                this.transform.position = temp;
            }
        }
        else
        {
            if (posY > -14.5f && posY < 16f)
            {
                Vector3 temp = this.transform.position;
                temp.y = posY;
                this.transform.position = temp;
            }
        }
    }
}
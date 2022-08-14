using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class Joystick : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    private float speed = 50f;
    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (gameObject.name == "Left" || gameObject.name == "Right")
        {
            player.setHorizontalMove(0);
        }
        if(gameObject.name == "Down")
        {

        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (gameObject.name == "Left")
        {
            player.setHorizontalMove(-speed);
        }
        if (gameObject.name == "Right")
        {
            player.setHorizontalMove(speed);
        }
        if (gameObject.name == "Up")
        {
            player.Jump();
        }
        if (gameObject.name == "Down")
        {
            player.Crounch();
        }
        if (gameObject.name == "Attack")
        {
            player.Shoot();
        }
    }
}

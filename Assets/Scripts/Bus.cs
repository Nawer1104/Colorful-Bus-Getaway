using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private int char_number = 0;

    public float speed = 10f;

    private bool canMove = false;

    public Transform posToMove;

    public GameObject vfxOnSuccess;

    private float min_X = -2.15f;

    private float max_X = 2.15f;

    private float min_Y = -4.85f;

    private float max_Y = 4.85f;

    public void PlusChar()
    {
        char_number += 1;

        if (char_number == 3)
        {
            canMove = true;

            //transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }
    }

    private void FixedUpdate()
    {
        CheckPos();

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMove.position, speed * Time.deltaTime);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "destroy")
        {
            if (char_number < 3) return;

            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].buses.Remove(this);
            GameObject explosion = Instantiate(vfxOnSuccess, transform.position, transform.rotation);
            Destroy(explosion, 2f);
            Destroy(gameObject);
        }
    }

    private void CheckPos()
    {
        if (transform.position.x < min_X)
        {
            Vector3 moveDirX = new Vector3(min_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X)
        {
            Vector3 moveDirX = new Vector3(max_X, transform.position.y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, min_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, max_Y, 0f);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, min_Y, 0f);
            transform.position = moveDirX;
        }
    }
}
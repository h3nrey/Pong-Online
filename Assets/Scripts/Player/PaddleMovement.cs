using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] [Range(0.1f, 2700f)]private float speed =2500;
    [SerializeField] private bool isPlayer = true;
    [SerializeField] private string playerAxis = "Arrows";
    Vector2 velocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (isPlayer) PlayerMovement(); else IAMovement();
    }

    void PlayerMovement() {
        float inputY = Input.GetAxisRaw(playerAxis);
        Vector2 paddleSpeed = inputY * speed * Time.deltaTime * Vector2.up;
        rb.velocity = paddleSpeed;
    }

    void IAMovement() {
        Vector2 followPoint = new Vector2(transform.position.x, BallMovement.ballInstance.transform.position.y);
        transform.position = Vector2.SmoothDamp(transform.position ,followPoint, ref velocity, 0.5f, speed);
    }
}

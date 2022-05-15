using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static BallMovement ballInstance;
    [SerializeField] Rigidbody2D ballRb;
    [SerializeField] float lauchForce;
    [SerializeField] Vector2 lauchDirection;
    [SerializeField] Vector2 ricochetDirection;

    private void Awake() {
        ballInstance = this;
    }

    void Start()
    {
        LauchBall();
    }

    public void LauchBall() {
        StartCoroutine(lauch());
        IEnumerator lauch() {
            yield return new WaitForSeconds(0.3f);
            ballRb.AddForce(lauchForce * generateRandomDirection(), ForceMode2D.Impulse);
        }
    }

    void Ricochet(Vector2 vel) {
        Vector2 dir = Vector2.zero;
        if(vel.y > 0) {
            dir = new Vector2(0, 1);
        } else if(vel.y < 0) {
            dir = new Vector2(0, -1);
        }
        ballRb.AddForce((lauchForce/5) * dir, ForceMode2D.Impulse);
    }

    Vector2 generateRandomDirection() {
        Vector2 dir = Vector2.zero;
        float valueX = Random.Range(1, 3);
        float valueY = Random.Range(1, 3);

        if (valueX < 2 && valueY < 2) {
            dir = new Vector2(-1, -0.8f);
        }
        else if (valueX < 2 && valueY >= 2) {
            dir = new Vector2(-1, 0.8f);
        }
        else if (valueX >= 2 && valueY < 2) {
            dir = new Vector2(1, -0.8f);
        }
        else if (valueX >= 2 && valueY >= 2) {
            dir = new Vector2(1, 0.8f);
            print(valueX + "---" + valueY);
        }
        return dir;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("goalL") || other.CompareTag("goalR")) {
            GameManager.game.restartElements();
            GameManager.game.ScoreUpdate(other.tag);
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("paddle")) {
            Ricochet(other.rigidbody.velocity);
        }
    }

    private void Update()
    {
        //print(ballRb.velocity);
    }
}

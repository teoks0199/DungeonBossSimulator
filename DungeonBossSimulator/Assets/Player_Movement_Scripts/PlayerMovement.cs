 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer _renderer;

    void Start() {
        animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
            if (Input.GetAxisRaw("Horizontal") > 0)
    {
        _renderer.flipX = true;
    }
    else if (Input.GetAxisRaw("Horizontal") < 0)
    {
        _renderer.flipX = false;
    }   
    TakeInput();
    Move();
    
}

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0) {
            SetAnimatorMovement(direction);
            animator.SetBool("isMoving", true);
        }

        else {
            animator.SetBool("isMoving", false);
        }
    }

    private void TakeInput() {

        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A)) {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction += Vector2.right;
        }

    }


    private void SetAnimatorMovement(Vector2 direction) {
        animator.SetLayerWeight(1,1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float speed = 0f;

    private Vector2 m_MoveDirection;
    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        m_MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (m_MoveDirection != Vector2.zero && m_MoveDirection.x != 0) {
            transform.rotation = Quaternion.Euler(0.0f, m_MoveDirection.x < 0 ? 0.0f : 180.0f, 0.0f);
        }
        m_Animator.SetBool("playerMoving", m_MoveDirection != Vector2.zero);
        Vector2 velocity = (m_MoveDirection.normalized * speed * Time.fixedDeltaTime);
        m_RigidBody.MovePosition(m_RigidBody.position + velocity);
    }
}

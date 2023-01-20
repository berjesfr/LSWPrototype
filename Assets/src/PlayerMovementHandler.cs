using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float speed = 0f;
    public AudioClip walkSound;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private AudioSource _source;
    private bool _playinFootstep;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = walkSound;
        _playinFootstep = false;
    }

    private void FixedUpdate() {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_moveDirection != Vector2.zero && _moveDirection.x != 0) {
            transform.rotation = Quaternion.Euler(0.0f, _moveDirection.x < 0 ? 0.0f : 180.0f, 0.0f);
        }
        _animator.SetBool("playerMoving", _moveDirection != Vector2.zero);
        Vector2 velocity = (_moveDirection.normalized * speed * Time.fixedDeltaTime);
        _rigidBody.MovePosition(_rigidBody.position + velocity);
        if (velocity != Vector2.zero && !_playinFootstep) {
            StartCoroutine("FootstepSound");
        }
    }

    private IEnumerator FootstepSound()
     {
        _playinFootstep = true;
        _source.Play();
        yield return new WaitForSeconds(_source.clip.length);
        _playinFootstep = false;
     }
}

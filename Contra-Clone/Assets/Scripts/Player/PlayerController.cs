using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
public float maxSpeed = 7;
public float jumpTakeOffSpeed = 7;
private SpriteRenderer _spriteRenderer;
private Animator _animator;
	// Use this for initialization
	void Awake () {
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected override void ComputeVelocity () 
	{
	Vector2 move = Vector2.zero;
	move.x =Input.GetAxis("Horizontal");
	if(Input.GetButtonDown("Jump") && grounded)	
	{
	velocity.y = jumpTakeOffSpeed;	
	} else if(Input.GetButtonUp("Jump"))
	{
		if(velocity.y > 0){
			velocity.y = velocity.y * 0.5f;
		}
	}

	if(move.x > 0.01f)
	{
		if(_spriteRenderer.flipX == true)
		{
			_spriteRenderer.flipX = true;
		}
		
	}
	else if(move.x < 0.01f)
	{
		if(_spriteRenderer.flipX == false)
		{
			_spriteRenderer.flipX = true;
		}
	}
	_animator.SetBool("grounded", grounded);
	_animator.SetFloat("velocityX", Mathf.Abs(velocity.x)/ maxSpeed);
	targetVelocity = move * maxSpeed;
	}
	
}

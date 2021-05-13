using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour {
public float speed;

private Rigidbody2D _rgbd2D;
private CircleCollider2D _circC;
private ParticleSystem _ps;
public bool enabledJump = true;
private bool _Isjumping = true;
private bool _isOnGround = false;
public float jumpForce = 2f;

public float maxStamina = 100f;
float currentStamina = 0f;

public float floatHeight;
    public float liftForce;
    public float damping;
	// Use this for initialization
	void Start () {
		_rgbd2D = GetComponent<Rigidbody2D>();
		_circC = GetComponent<CircleCollider2D>();
		_ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	//Move character
	float moveHorizontal = Input.GetAxis("Horizontal");
	float moveVertical = Input.GetAxis("Vertical");
	if(enabledJump ==false)
	{
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0  )
		{
			//move up by using w,s or arrow up and down
			_rgbd2D.velocity = new Vector2(moveHorizontal * speed,moveVertical * speed);
		}
		else{

			_rgbd2D.velocity = new Vector2(moveHorizontal * 0,moveVertical * 0);
		}
	}

	if (enabledJump == true)
	{
		if(Input.GetAxis("Horizontal") != 0)
		{
			// Enable jump
			_rgbd2D.velocity = new Vector2(moveHorizontal* speed, transform.position.y);
		}
		else
		{
			_rgbd2D.velocity = new Vector2(moveHorizontal * 0, transform.position.y);
		}

		//Jump button
		
		if(Input.GetAxis("Fire2") == 1f)
		{
			JumpPlayer(true);
		}
		else{
			JumpPlayer(false);
		}

	}
		
		if(Input.GetAxis("Fire1") == 1f)
		{
			disableCircleCollider2D(true);
		}
		else
		{
			disableCircleCollider2D(false);
		}

	}
	public bool disableCircleCollider2D(bool firetrue)
	{
		if(firetrue == true)
		{
			_circC.isTrigger = true;
			ParticleColorOfLiveTime(true);
		}
		else{
			_circC.isTrigger = false;
			ParticleColorOfLiveTime(false);
		}
		
		return firetrue;
	}

	public bool JumpPlayer(bool jumpTrue)
	{
		if(jumpTrue == true)
		{
			_rgbd2D.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
			Debug.Log("HYPPÄÄN");
		}
		else
		{
			Debug.Log("EN HYPPÄÄ");
		}
		return jumpTrue;
	}

	public bool ParticleColorOfLiveTime(bool changecolor)
	{
		if(changecolor == true)
		{
			var col = _ps.colorOverLifetime;
			col.enabled = true;

			Gradient grad = new Gradient();
			grad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );

			col.color = grad;
		}
		else
		{
			var col = _ps.colorOverLifetime;
			col.enabled = false;
		}
		return changecolor;
	}
}

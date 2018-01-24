using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 7;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	public Vector2 directionalInput;
 
    bool wallSliding;
    bool facingright=true;
    bool jumped;
    bool doublejumped;
    int wallDirX;
    public AudioClip[] jumpClips;
    private Animator anim;
	void Start() {
		controller = GetComponent<Controller2D> ();
        anim = GetComponent<Animator>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

	}
    void Flip(float horizontal)
    {
        if (!wallSliding && (horizontal > 0 && !facingright || horizontal < 0 && facingright))
        {
            facingright = !facingright;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }

    void Update() {
        if (controller.collisions.below)
        {
            doublejumped = false;
            //anim.SetBool("Jump", false);
            anim.SetBool("Jump2", false);
        }
        CalculateVelocity ();
		HandleWallSliding ();

		controller.Move (velocity * Time.deltaTime, directionalInput);
       
        Flip(directionalInput.x);
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
        anim.SetFloat("Speed",Mathf.Abs(directionalInput.x));
        anim.SetBool("Grounded", controller.collisions.below);
        anim.SetBool("wallSliding", wallSliding);
    }

    public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
  
        if (wallSliding) {
            AudioSource.PlayClipAtPoint(jumpClips[2], transform.position);
            if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}

        if (controller.collisions.below) {
            velocity.y = maxJumpVelocity;
            anim.SetTrigger("Jump");
            AudioSource.PlayClipAtPoint(jumpClips[0], transform.position);
        }
        if (!controller.collisions.below && !doublejumped && !wallSliding)
        {
            AudioSource.PlayClipAtPoint(jumpClips[1], transform.position);
            anim.SetTrigger("Jump2");
            velocity.y = maxJumpVelocity;
            doublejumped = true;
        }
     
   
    }

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}
}

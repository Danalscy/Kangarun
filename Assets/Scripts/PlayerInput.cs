using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
        Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        player.SetDirectionalInput (directionalInput);
     
        if (Input.GetKeyDown (KeyCode.Space) ) {
			player.OnJumpInputDown ();
		}
		/*if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}*/
	}
    public void LeftArrow () {
        player.directionalInput.x = -1;

    }
    public void RightArrow()
    {

        player.directionalInput.x = 1;
    }
    public void Unpressed()
    {
        Vector2 directionalInput = new Vector2(0, 0);
        player.SetDirectionalInput(directionalInput);
    }
    public void Jump()
    {
        player.OnJumpInputDown();
        player.directionalInput.y = 1;
    }

}

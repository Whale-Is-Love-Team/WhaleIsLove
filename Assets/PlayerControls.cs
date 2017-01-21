using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {


    public float speed = 6.0f;
    public bool grounded = false;
    public bool movesInX = false;
    public bool movesInY = true;

    public float initialXPositionInUnits = 0;
    public float initialYPositionInUnits = 0;

    private GameObject player;
    private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
        this.player = this.gameObject;
        this.player.transform.position = new Vector3(initialXPositionInUnits, 0, initialYPositionInUnits);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!grounded)
        {
            this.moveDirection = new Vector3(0, 0, 0);
            if (movesInX) this.moveDirection.x = Input.GetAxis("Horizontal");
            if (movesInY) this.moveDirection.y = Input.GetAxis("Vertical");
            this.moveDirection *= this.speed;
        }
        this.player.transform.Translate(this.moveDirection * Time.deltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed; 

	
	// Update is called once per frame
	void Update () {

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        transform.Translate(movement * speed);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyController : MonoBehaviour {

    // Inspector variables
    [Range(0,6)][SerializeField] float edgeOffset;

    // Private variables
    private Vector2 viewportSize;
    private float xBounds;

    // Sets private variables
    private void Awake()
    {
        Camera camera = Camera.main;
        viewportSize.x = camera.orthographicSize * camera.aspect;
        xBounds = viewportSize.x;
    }

    // Calls move function every tenth of a second
    private void Start()
    {
        InvokeRepeating("moveStatic", 0, 0.1f);
    }

    // Changes direction of all children at once
    private void moveStatic()
    {

        bool check = false;

        // Loops through all children
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Checks if their position is bigger/smaller than the maximum size of the camera
            if (this.transform.GetChild(i).transform.position.x > xBounds - this.transform.GetChild(i).GetComponent<BoxCollider2D>().size.x
                * this.transform.GetChild(i).transform.localScale.x / 2 - edgeOffset ||
                this.transform.GetChild(i).transform.position.x < -xBounds + this.transform.GetChild(i).GetComponent<BoxCollider2D>().size.x
                * this.transform.GetChild(i).transform.localScale.x / 2 + edgeOffset)
            { check = true; }
        }

        // Changes direction of all children if one child has hit the bounds
        if (check)
        {
            ChangeDirection();
            check = false;
        }
    }
    
    // Changes direction of all children
    private void ChangeDirection()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<EnemyController>().ChangeDirection();
        }
    }
}

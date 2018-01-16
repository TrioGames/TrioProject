using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2? touchDeltaPosition = getTouchPosition();
        Vector2? mouseDeltaPosition = getMousePosition();

        if (touchDeltaPosition == null && mouseDeltaPosition == null)
        {
            return;
        }

        if (Gamer.instance.GameStatus == Constants.GAME_STATUS_SLOWMO)
        {
            return;
        }

        Gamer.instance.StartGame();
        Vector2 position = touchDeltaPosition != null? (Vector2) touchDeltaPosition : (Vector2) mouseDeltaPosition;

        int quarter = getTouchedQuarter(position);
        GameObject mainball = GameObject.Find(Constants.MAIN_BALL);

        // Vector3 dir = getForceDirection(quarter);
        // mainball.GetComponent<Rigidbody>().AddForce(dir * 200);

        int dir = getVelocityDirection(quarter);
        if (mainball.GetComponent<Rigidbody>().velocity.y < 10)
        {
            mainball.GetComponent<Rigidbody>().velocity = new Vector3(1 * dir, 3, mainball.GetComponent<Rigidbody>().velocity.z);
        }
    }

    int getVelocityDirection(int quarter)
    {
        if (quarter == 1)
        {
            return -1;
        }
        else if (quarter == 2)
        {
            return -1;
        }
        else if (quarter == 3)
        {
            return 1;
        }
        else if (quarter == 4)
        {
            return 1;
        }
        return 0;
    }

    Vector3 getForceDirection(int quarter)
    {
        Vector3 dir = transform.forward;
        if (quarter == 1)
        {
            dir = Quaternion.AngleAxis(60, Vector3.forward) * Vector3.right;
        }
        else if (quarter == 2)
        {
            dir = Quaternion.AngleAxis(75, Vector3.forward) * Vector3.right;
        }
        else if (quarter == 3)
        {
            dir = Quaternion.AngleAxis(105, Vector3.forward) * Vector3.right;
        }
        else if (quarter == 4)
        {
            dir = Quaternion.AngleAxis(120, Vector3.forward) * Vector3.right;
        }
        return dir;
    }

    int getTouchedQuarter(Vector2 position)
    {
        // Need to put .x
        int quarter = (Screen.width / 4);
        if (position.x <= quarter)
        {
            return 1;
        }
        else if (position.x <= (2 * quarter))
        {
            return 2;
        }
        else if (position.x <= (3 * quarter))
        {
            return 3;
        }
        else if (position.x <= (4 * quarter))
        {
            return 4;
        }
        return 0;
      
    }

    Vector2? getTouchPosition()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            return Input.GetTouch(0).position;
        }
        return null;
    }

    Vector2? getMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        return null;
    }
}

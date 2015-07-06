using UnityEngine;
using System.Collections;

public class Drawer : MonoBehaviour {

	private Vector3 start;
	private Vector3 end;
	public GameObject platform;
	private Vector3 axisX;
	private float angle, scale; 

	// Use this for initialization
	void Start () {
		axisX = new Vector3 (1, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
		{
			start = Camera.main.WorldToViewportPoint(Input.mousePosition);
		}
		
		if (Input.GetMouseButtonUp (0)) 
		{
			end = Camera.main.WorldToViewportPoint(Input.mousePosition);
			scale = Vector3.Distance (end, start);
			Vector3 v = (end - start);
			v.Normalize();
			Debug.Log(v.normalized);
			if(end.y >= start.y)
			{
				angle = Vector3.Angle (axisX, end - start);
			}else{
				angle = -Vector3.Angle (axisX, end - start);
			}
		}
	}
}

 #pragma strict
 var quad : GameObject;
 var circle : GameObject;
   
   
 //http://answers.unity3d.com/questions/631342/drawing-with-the-mouse-without-gaps.html 
  
 private var prevPos : Vector3;
 private var startPos : Vector3;
 private var length : float;
 private var nextDrag : float = -0.01;
 
 function Awake()
 {
     //gameFont = gameObject.GetComponent(gamer); //get the c#script
 }
 
 function Update()
 {
     if (Input.GetMouseButtonDown(0)) {
        prevPos = Input.mousePosition;
        prevPos = Camera.main.ScreenToWorldPoint(prevPos);
        prevPos.z = -17.0f;
        startPos  = prevPos;
        length = 0;
     }
     else if (Input.GetMouseButton(0) && Time.time > nextDrag) {
        var pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
  		pos.z = -17.0f;  		
        if (pos != prevPos && length < 0.7f) {
            if (prevPos.y < pos.y + 0.15f || prevPos.y > pos.y - 0.15f  || prevPos.x < pos.x - 0.15f || prevPos.x > pos.x + 0.15f )
            {
             	var dir = pos - prevPos;
             	var go = Instantiate(quad);
             	go.transform.position = prevPos + dir / 2.0;
             	var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
             	go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
             	go.transform.localScale.x = dir.magnitude;
             	length += CalculateLengthOfLine(pos,prevPos);
            }
            else
            {
                var goCircle = Instantiate(circle);
                goCircle.transform.position = pos;
            }
            prevPos = pos;
        }
     }
     else if (Input.GetMouseButtonUp(0))
     {
     	nextDrag = Time.time + 0.5;
     }
 }
 
 function CalculateLengthOfLine(pos1 : Vector3,pos2 : Vector3)
 {
 	var length = Mathf.Sqrt(Mathf.Pow( pos1.x - pos2.x, 2 ) + Mathf.Pow( pos1.y - pos2.y, 2 ));
 	return length;  	
 }
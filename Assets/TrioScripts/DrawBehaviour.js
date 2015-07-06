 #pragma strict
 var quad : GameObject;
 var circle : GameObject;
  
 //http://answers.unity3d.com/questions/631342/drawing-with-the-mouse-without-gaps.html 
  
 private var prevPos : Vector3;
  
 function Update()
 {
     if (Input.GetMouseButtonDown(0)) {
        prevPos = Input.mousePosition;
        prevPos = Camera.main.ScreenToWorldPoint(prevPos);
        prevPos.z = -17.0f;
     }
     if (Input.GetMouseButton(0)) {
        var pos = Input.mousePosition;
        
        pos = Camera.main.ScreenToWorldPoint(pos);
  		pos.z = -17.0f;
  		
        if (pos != prevPos) {
        
            if (prevPos.y < pos.y || prevPos.y > pos.y  || prevPos.x < pos.x || prevPos.x > pos.x )
            {
                var dir = pos - prevPos;
             var go = Instantiate(quad);
             go.transform.position = prevPos + dir / 2.0;
             var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
             go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
             go.transform.localScale.x = dir.magnitude;
            }
            else
            {
                //var goCircle = Instantiate(circle);
                //goCircle.transform.position = pos;
            }
            prevPos = pos;
        }
     }
 }
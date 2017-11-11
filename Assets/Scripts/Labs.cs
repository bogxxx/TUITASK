using System    .Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labs : MonoBehaviour {
    public Camera camera;
    public string v11, v12, v13, a11, a12, a13, r21, r22, r23, a21, a22, a23;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (/*Input.GetAxis("Mouse ScrollWheel") < 0*/ Input.GetKeyDown(KeyCode.DownArrow) && camera.transform.position.y > -39)
        camera.transform.SetPositionAndRotation(new Vector3(camera.transform.position.x, camera.transform.position.y - 1, -1), new Quaternion());        
        if (/*Input.GetAxis("Mouse ScrollWheel") > 0*/ Input.GetKeyDown(KeyCode.UpArrow) && camera.transform.position.y < 0)
        camera.transform.SetPositionAndRotation(new Vector3(camera.transform.position.x, camera.transform.position.y + 1, -1), new Quaternion());

        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

    void OnGUI()
    {
        //1st table
        v11 = GUI.TextField(new Rect((-5.4f - Camera.main.ScreenToWorldPoint(camera.transform.position).x)*Screen.width/17.3f, (-(-50.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).y)*Screen.height/10), Screen.width/5.5f, Screen.height/20f), v11);
        v12 = GUI.TextField(new Rect((-2.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-50.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f), v12);
        v13 = GUI.TextField(new Rect((1.2f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-50.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f), v13);
        a11 = GUI.TextField(new Rect((-5.4f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-50.7f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f), a11);
        a12 = GUI.TextField(new Rect((-2.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-50.7f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f), a12);
        a13 = GUI.TextField(new Rect((1.2f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-50.7f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f), a13);

        //2nd table
        r21 = GUI.TextField(new Rect((-3.2f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 6.15f, Screen.height / 20f), r21);
        r22 = GUI.TextField(new Rect((-0.23f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 6.15f, Screen.height / 20f), r22);
        r23 = GUI.TextField(new Rect((2.66f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.1f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 6.15f, Screen.height / 20f), r23);
        a21 = GUI.TextField(new Rect((-3.2f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.7f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 6.15f, Screen.height / 20f), a21);
        a22 = GUI.TextField(new Rect((-0.23f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.7f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 6.15f, Screen.height / 20f), a22);
        a23 = GUI.TextField(new Rect((2.66f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.7f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 6.15f, Screen.height / 20f), a23);
        if (v11 == "1" && a11 == "0.5" && v12 == "2" && a12 == "1" && v13 == "3" && a13 == "1.5")
        {
            GUI.TextArea((new Rect((5.2f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-50.4f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f)), "пошел нахуй");
        }
        if (r21 != "" && r22 != "" && r23 != "" && a21 != "" && a22 != "" && a23 != "")
        {
            var ar12 = System.Convert.ToInt32(a12);
            if (int.Parse(r21) / int.Parse(a21) == 5 && int.Parse(r22) / int.Parse(a22) == 20 && int.Parse(r23) / int.Parse(a23) == 80)
            {
                GUI.TextArea((new Rect((5.2f - Camera.main.ScreenToWorldPoint(camera.transform.position).x) * Screen.width / 17.3f, (-(-53.4f - Camera.main.ScreenToWorldPoint(camera.transform.position).y) * Screen.height / 10), Screen.width / 5.5f, Screen.height / 20f)), "пошел нахуй");
            }
        }
    }
}

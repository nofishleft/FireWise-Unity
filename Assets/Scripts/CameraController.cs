using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float cameraWidth;
    public float cameraHeight;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        Vector3 par = transform.parent.position;
        Vector3 temp= new Vector3(par.x - Mathf.Clamp(par.x, minX + 0.5f * cameraWidth, maxX - 0.5f * cameraWidth), par.y - Mathf.Clamp(par.y, minY + 0.5f * cameraHeight, maxY - 0.5f * cameraHeight), transform.localPosition.z);
        temp.z = -temp.z;
        transform.localPosition = -temp;
    }
}

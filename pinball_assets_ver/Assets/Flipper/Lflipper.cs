using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lflipper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //hingeJoint2D = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    public void Update()
    {

    if (Input.GetKey(KeyCode.Space)) {
        //hingeJoint2D.Angle=transform.Rotate(0, 0, 15);
        transform.Rotate(new Vector3(0, 0, -20));
    }else {
        //transform.Rotate(new Vector3(0, 0, -20));
    }


        //transform.Rotate(new Vector3(0, 0, 5));
    }
}

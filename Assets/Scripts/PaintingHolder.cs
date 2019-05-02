using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PaintingHolder : MonoBehaviour {
    public Text debugText;
    public GameObject m_painting;
    public Vector3 m_initialPaintingTransform;
    public Vector3 m_initialPaintingRotation;
    public GameObject paintingPrefab;

    void OnTriggerEnter(Collider other)
    {   
        /*if (other.gameObject.name == ("HospitalPainting")) 
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Rigidbody>().MovePosition(m_initialPaintingTransform);
        }*/
        /*if(other.gameObject.name == "HospitalPainting")
        {
            transform.GetComponent<FixedJoint>().connectedBody = m_painting.GetComponent<Rigidbody>();
            other.gameObject.transform.localPosition = m_initialPaintingTransform;
            other.gameObject.transform.localRotation = Quaternion.Euler(m_initialPaintingRotation);
        }*/
		
    }
	// Use this for initialization
	void Start () {
        //debugText.text = DateTime.Now.ToString();
        m_initialPaintingTransform = new Vector3(2.243f, 1.298f, 2.891f);
        m_initialPaintingRotation = new Vector3(0, 0, 0);
        /*m_initialPainingTransform = m_painting.transform.localPosition;
                m_initialPaintingRotation = m_painting.transform.localRotation.eulerAngles; */
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

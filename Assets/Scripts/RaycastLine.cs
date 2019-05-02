using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RaycastLine : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    // 1
    public GameObject raycastPrefab;
    // 2
    private GameObject raycast;
    // 3
    private Transform raycastTransform;
    // 4
    private Vector3 hitPoint;

    public GameObject collidingObject;

    private GameObject objectInHand;

    public Transform headTransform;

    public GameObject paintingHolder;

    public GameObject painting;

    public Text debugText;

    public float distance;

    public GameObject m_paintingPrefab;

    public Vector3 m_paintingSnapPosition;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
        // 1
        raycast = Instantiate(raycastPrefab);
        // 2
        raycastTransform = raycast.transform;

    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
       
    }


    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        collidingObject.transform.position = new Vector3(trackedObj.transform.position.x, trackedObj.transform.position.y, trackedObj.transform.position.z);
        // 1
        objectInHand = collidingObject;
        collidingObject = null;

        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
       
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4
        objectInHand = null;
    }


    void Update()
    {
        distance = Vector3.Distance(painting.transform.localPosition, paintingHolder.transform.localPosition);
        debugText.text = "Distance : " + distance.ToString();
        RaycastHit hit;

        if (Controller.GetHairTriggerDown())
        {
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 150))
            {
                hitPoint = hit.point;
                ShowRaycast(hit);
                if (hit.collider.gameObject != null)
                {
                    collidingObject = hit.collider.gameObject;
                    if (collidingObject.GetComponent<Rigidbody>() != null && hit.distance<=1f)
                    { 
                    if (collidingObject.gameObject.name == "tornPainting")
                    {
                        collidingObject.GetComponent<Rigidbody>().isKinematic = false;
                    }

                    GrabObject();
                }
                }
            }

        }
        else
        {
            raycast.SetActive(false);
        }



        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                if (objectInHand.gameObject.name == "tornPainting" && distance <= 0.5f)
                {
                    objectInHand.GetComponent<Rigidbody>().isKinematic = true;
                    objectInHand.gameObject.transform.localPosition = m_paintingSnapPosition;
                    objectInHand.gameObject.transform.localRotation = Quaternion.identity;

                }
                ReleaseObject();
            }

        }
    }


    private void ShowRaycast(RaycastHit hit)
    {
        // 1
        raycast.SetActive(true);
        // 2
        raycastTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // 3
        raycastTransform.LookAt(hitPoint);

        // 4
        raycastTransform.localScale = new Vector3(raycastTransform.localScale.x, raycastTransform.localScale.y,
            hit.distance);
    }
}

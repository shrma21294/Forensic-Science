using UnityEngine;
using UnityEngine.UI;
public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedOj;

    public GameObject collidingObject;

    private GameObject objectInHand;

    public GameObject paintingHolder;

    public GameObject painting;

    public Text debugText;

    public float distance;

    public GameObject m_paintingPrefab;

    public Vector3 m_paintingSnapPosition;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedOj.index); }
    }

    void Awake()
    {
        trackedOj = GetComponent<SteamVR_TrackedObject>();
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

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(painting.transform.localPosition, paintingHolder.transform.localPosition);
        debugText.text = "Distance : " + distance.ToString();
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                if (collidingObject.gameObject.name == "tornPainting")
                {
                    collidingObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                GrabObject();
            }
        }

        // 2
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
}

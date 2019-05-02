using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHighlightTest : MonoBehaviour {

	private Shader shader1;
	private Shader shader2;
	public Renderer rend;

    public GameObject lastHit;

    public GameObject uvLight;

	public Text debugText;

    public GameObject roomLights;

    [Range(0,1)]
    public float highlightIntensity = 0.5f;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	public GameObject laserPrefab;
	// 2
	private GameObject laser;
	// 3
	private Transform laserTransform;
	// 4
	private Vector3 hitPoint; 

	void Start() {
		shader1 = Shader.Find("Standard");
		shader2 = Shader.Find("Outlined/Silhouetted Diffuse");
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private void ShowLaser(RaycastHit hit)
	{

		laser.SetActive(true);
		// 2
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
		// 3
		laserTransform.LookAt(hitPoint); 
		// 4
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
			hit.distance);
	}
	
	// Update is called once per frame
	void Update () {
		if (uvLight.activeInHierarchy)
		{
			RaycastHit hit;
			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 50) && hit.collider.GetComponent<GlowHighlightScript>()) {
               // debugText.text = "RayCast Hit" + hit.collider.name;
                lastHit = hit.collider.gameObject;
				hit.collider.GetComponent<GlowHighlightScript> ().Highlight (Color.blue, highlightIntensity);       
            }
            else
            {
               // debugText.text = "RayCast not Hit " + lastHit.name;
                lastHit.GetComponent<GlowHighlightScript>().Highlight(Color.black, 0.0f);
            }
        }
		else // 3
		{
			//laser.SetActive(false);
		}
	}
}

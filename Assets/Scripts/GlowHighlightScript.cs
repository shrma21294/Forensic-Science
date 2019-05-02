using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowHighlightScript : MonoBehaviour {

	private Renderer renderer;
	private Material mat;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();
		mat = renderer.material;
	}

	public void Highlight(Color t_hightlightColor, float emissionValue) {
        if (emissionValue == 0.0f)
            Debug.Log(gameObject.name);
		Color baseColor = t_hightlightColor; 
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emissionValue);
		mat.SetColor ("_EmissionColor", finalColor);
	}
}

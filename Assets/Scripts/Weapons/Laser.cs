using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public Transform mainPart;
	public Transform glowPart;

	public Color color = Color.white;
	public Color glowColor = Color.red;

	public string sortingLayer;
	public int orderLayer;

	LineRenderer lineRenderer;
	LineRenderer lineRendererGlow;

	void Awake() {
		lineRenderer = mainPart.GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount (2);
		lineRenderer.sortingLayerName = sortingLayer;
		lineRenderer.sortingOrder = orderLayer;

		lineRendererGlow = glowPart.GetComponent<LineRenderer>();
		lineRendererGlow.SetVertexCount (2);
		lineRendererGlow.sortingLayerName = sortingLayer;
		lineRendererGlow.sortingOrder = orderLayer - 1;

		SetColor ();
	}

	public void Create (Vector3 startPosition, Vector3 endPosition, float width, float widthGlow) {	
		lineRenderer.SetWidth(width, width);
		lineRendererGlow.SetWidth(widthGlow, widthGlow);

		lineRenderer.SetPosition(0, startPosition);
		lineRenderer.SetPosition(1, endPosition);
		lineRendererGlow.SetPosition(0, startPosition);
		lineRendererGlow.SetPosition(1, endPosition);

		lineRenderer.enabled = true;
		lineRendererGlow.enabled = true;
	}

	void SetColor() {
		lineRenderer.material.SetColor("_Color", color);
		lineRendererGlow.material.SetColor("_Color", glowColor);
	}

	public void Clear() {
		lineRenderer.enabled = false;
		lineRendererGlow.enabled = false;
	}
}

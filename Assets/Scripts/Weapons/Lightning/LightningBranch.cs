using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LightningBranch : MonoBehaviour {

	public Transform StartPart;
	public Transform CenterPart;
	public Transform EndPart;
	public Transform StartPartGlow;
	public Transform CenterPartGlow;
	public Transform EndPartGlow;

	LineRenderer startRenderer;
	LineRenderer centerRenderer;
	LineRenderer endRenderer;
	LineRenderer startRendererGlow;
	LineRenderer centerRendererGlow;
	LineRenderer endRendererGlow;

	Color colorGlow = Color.white;
	Color colorMain = Color.white;

	bool isStart = false;
	bool isEnd = false;

	float maxLifeTime = 0.6f;
	float lifeTime = 0f;
	float initialAlpha = 1.0f;

	public void CreateLightningBranch(List<Vector3> positions, Color color, float alpha, float width, float widthGlow, float duration, string nameLayer, int sortLayer){
		startRenderer = StartPart.GetComponent<LineRenderer>();
		centerRenderer = CenterPart.GetComponent<LineRenderer>();
		endRenderer = EndPart.GetComponent<LineRenderer>();

		startRendererGlow = StartPartGlow.GetComponent<LineRenderer>();
		centerRendererGlow = CenterPartGlow.GetComponent<LineRenderer>();
		endRendererGlow = EndPartGlow.GetComponent<LineRenderer>();

		Vector3[] vertexes = positions.ToArray ();
		int vertexCount = vertexes.Length;
		centerRenderer.SetVertexCount(vertexCount);
		centerRenderer.SetPositions(vertexes);
		centerRendererGlow.SetVertexCount(vertexCount);
		centerRendererGlow.SetPositions(vertexes);
				
		if(vertexCount >= 1){
			StartPart.position = vertexes[0] + transform.position;
			StartPartGlow.position = vertexes[0] + transform.position;
			EndPart.position = vertexes[vertexCount - 1] + transform.position;
			EndPartGlow.position = vertexes[vertexCount - 1] + transform.position;
			
			startRenderer.SetPosition(0, new Vector3(-width/2, 0, 0));
			startRendererGlow.SetPosition(0, new Vector3(-widthGlow/2, 0, 0));
			endRenderer.SetPosition(1, new Vector3(width/2, 0, 0));
			endRendererGlow.SetPosition(1, new Vector3(widthGlow/2, 0, 0));
		}

		if(vertexCount > 1){
			Vector3 vector = (vertexes[1] - vertexes[0]).normalized;
			float startRotationAngle = Vector3.Angle(Vector3.right, vector) * (vector.y < 0 ? -1 : 1);
			StartPart.Rotate(Vector3.forward, startRotationAngle);
			StartPartGlow.Rotate(Vector3.forward, startRotationAngle);

			vector = (vertexes[vertexCount - 1] - vertexes[vertexCount - 2]).normalized;
			float endRotationAngle = Vector3.Angle(Vector3.right, vector) * (vector.y < 0 ? -1 : 1);
			EndPart.Rotate(Vector3.forward, endRotationAngle);
			EndPartGlow.Rotate(Vector3.forward, endRotationAngle);
		}

		startRenderer.sortingLayerName = nameLayer;
		startRenderer.sortingOrder = sortLayer;
		startRendererGlow.sortingLayerName = nameLayer;
		startRendererGlow.sortingOrder = sortLayer - 1;

		centerRenderer.sortingLayerName = nameLayer;
		centerRenderer.sortingOrder = sortLayer;
		centerRendererGlow.sortingLayerName = nameLayer;
		centerRendererGlow.sortingOrder = sortLayer - 1;
		
		endRenderer.sortingLayerName = nameLayer;
		endRenderer.sortingOrder = sortLayer;
		endRendererGlow.sortingLayerName = nameLayer;
		endRendererGlow.sortingOrder = sortLayer - 1;
		
		startRenderer.SetWidth(width, width);
		centerRenderer.SetWidth(width, width);
		endRenderer.SetWidth(width, width);
		startRendererGlow.SetWidth(widthGlow, widthGlow);
		centerRendererGlow.SetWidth(widthGlow, widthGlow);
		endRendererGlow.SetWidth(widthGlow, widthGlow);

		isStart = true;
		maxLifeTime = duration * alpha;
		initialAlpha = alpha;
		colorGlow = color;

		SetColor();
	}

	void FixedUpdate () {
		if(isEnd){
			LineRenderer[] lineRenderers = gameObject.GetComponentsInChildren<LineRenderer>();

			for(int i = lineRenderers.Length - 1; i >= 0; i--)
				DestroyImmediate(lineRenderers[i].material);

			DestroyImmediate(gameObject);
		}
		else if(isStart) {
			lifeTime += Time.deltaTime;

			if(lifeTime >= maxLifeTime) {
				lifeTime = maxLifeTime;
				isEnd = true;
			}

			SetColor();
		}
	}

	void SetColor(){
		colorGlow.a = initialAlpha * (maxLifeTime - lifeTime)/maxLifeTime;
		colorMain.a = initialAlpha * (maxLifeTime - lifeTime)/maxLifeTime * 2;
		
		startRendererGlow.material.SetColor("_Color", colorGlow);
		centerRendererGlow.material.SetColor("_Color", colorGlow);
		endRendererGlow.material.SetColor("_Color", colorGlow);
		
		startRenderer.material.SetColor("_Color", colorMain);
		centerRenderer.material.SetColor("_Color", colorMain);
		endRenderer.material.SetColor("_Color", colorMain);
	}
}

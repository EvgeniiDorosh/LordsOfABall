using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightning : MonoBehaviour {
	
	public GameObject lightningBranch;
	public string sortingLayer;
	public int orderLayer;
	[Range(0.05f, 5.0f)]
	public float MaxTimeLifeLightning = 0.3f;
	[Space(20)]
	public Color ColorLightning = Color.red;
	[Range(1, 6)]
	public int QuantityIterations = 5;
	[Range(0.0f, 1.0f)]
	public float OffsetLine = 0.2f;
	[Range(0.0f, 10.0f)]
	public float OffsetPlusDistanseLine = 0.0f;
	[Range(0.0f, 180.0f)]
	public float AngleAdditionalLightning = 30.0f;
	[Range(0.0f, 1.0f)]
	public float LengthScaleAdditionalLightning = 0.9f;
	[Range(0.0f, 1.0f)]
	public float ProbabilityAdditionalLightning = 0.5f;
	[Range(0.01f, 1.0f)]
	public float WidthLightning = 0.148f;
	[Range(0.01f, 1.0f)]
	public float WidthLightningGlow = 0.148f;

	public void Create (Vector3 startPosition, Vector3 endPosition) {
		float a = 1.0f;
		float _OffsetLine = OffsetLine * (Vector3.Distance(startPosition, endPosition) / 2 + OffsetPlusDistanseLine);
		float widthLightning = WidthLightning;
		float widthLightningGlow = WidthLightningGlow;

		List<List<LightningBranchPiece>> wholeLightning = new List<List<LightningBranchPiece>>();

		List<LightningBranchPiece> molniya0 = new List<LightningBranchPiece>();
		molniya0.Add(new LightningBranchPiece() {
			startPosition = startPosition,
			endPosition = endPosition,
			a = a,
			width = widthLightning,
			widthGlow = widthLightningGlow
		});
		wholeLightning.Add(molniya0);

		for(int i = 0; i < QuantityIterations; i++) {
			int k_p = wholeLightning.Count;
			for(int p = 0; p < k_p; p++)  {
				int k = wholeLightning[p].Count;
				for(int j = 0; j < k; j++)  { 
					Vector3 sp = wholeLightning[p][j].startPosition;
					Vector3 ep = wholeLightning[p][j].endPosition;
					a = wholeLightning[p][j].a;
					widthLightning = wholeLightning[p][j].width;
					widthLightningGlow = wholeLightning[p][j].widthGlow;

					Vector3 midP = sp + (ep - sp)/2;
					Vector3 norm = (ep - sp).normalized;

					midP += new Vector3(norm.y, -norm.x, 0) * Random.Range(-_OffsetLine, _OffsetLine);
					
					wholeLightning[p][j].endPosition = midP;
					wholeLightning[p].Insert(j+1, new LightningBranchPiece() { startPosition = midP, endPosition = ep, a = a, width = widthLightning, widthGlow = widthLightningGlow });
					j++;
					k++;

					if(Random.Range(0.0f, 1.0f) <= ProbabilityAdditionalLightning) {
						Vector3 direction = midP - sp;
						float randomAngleLine = Random.Range(-AngleAdditionalLightning, AngleAdditionalLightning);
						direction = Quaternion.Euler(0, 0, randomAngleLine) * direction;						
						Vector3 splitEnd = direction*LengthScaleAdditionalLightning + midP; 
						molniya0 = new List<LightningBranchPiece>();
						molniya0.Add(new LightningBranchPiece() { startPosition = midP, endPosition = splitEnd, a = a, width = widthLightning/2.5f, widthGlow = widthLightningGlow/1.5f });
						wholeLightning.Add(molniya0);
					}
				}
			}
			_OffsetLine /= 2;
		}

		foreach(List<LightningBranchPiece> lms in wholeLightning) {
			List<Vector3> l_v2 = new List<Vector3>();

			float _a = lms[0].a;
			float _widthLightning = lms[0].width;
			float _widthLightning_glow = lms[0].widthGlow;

			l_v2.Add(lms[0].startPosition);
			foreach(LightningBranchPiece lm in lms) {
				l_v2.Add(lm.endPosition);
			}

			GameObject go = Instantiate(lightningBranch);
			go.transform.SetParent(gameObject.transform, false);
			LightningBranch mol = go.GetComponent<LightningBranch>();
			mol.CreateLightningBranch(l_v2, ColorLightning, _a, _widthLightning, _widthLightning_glow, MaxTimeLifeLightning, sortingLayer, orderLayer);
		}
	}

}

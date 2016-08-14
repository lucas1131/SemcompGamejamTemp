using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodSpiller : MonoBehaviour {

	public float spillRange = 0.5f;
	public int spillCount = 3;
	public BloodSpill bloodSpillPrefab;


	public void SpillBlood() {
		for (int i = 0; i < spillCount; i++){
			Transform t = transform;
			t.position = new Vector2 (
				t.position.x + Random.Range (-spillRange, spillRange),
				t.position.y + Random.Range (-spillRange, spillRange));
			BloodSpill g = (BloodSpill)Instantiate (bloodSpillPrefab);
			g.transform.position = t.transform.position;
			g.transform.Rotate(0, 0, Random.Range(0, 360)); 
		}
	}
}

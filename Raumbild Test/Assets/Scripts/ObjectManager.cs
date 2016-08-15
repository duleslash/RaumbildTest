using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectManager : MonoBehaviour {

	public GameObject[] objects;
	public Transform markerparent;
	public void Create (int i) {
		GameObject g = Instantiate (objects [i]) as GameObject;
		g.transform.parent=markerparent;
		g.transform.localPosition = Vector3.zero;
		g.transform.localEulerAngles = Vector3.zero + Vector3.up*(180+Random.Range(-45,45));
		g.transform.localScale = Vector3.one;
		GetComponent<TouchCtrl> ().Set (g.transform);
		furnitures.indexes.Add (i);
		furnitures.transforms.Add (g.transform);
		furnitures.rotations.Add (Quaternion.identity);
		furnitures.positions.Add (Vector3.zero);
		furnitures.scales.Add (Vector3.one);
	}
	public void Create (int i,Vector3 pos,Quaternion rot,Vector3 scale) {
		GameObject g = Instantiate (objects [i]) as GameObject;
		g.transform.parent=markerparent;
		g.transform.localPosition = pos;
		g.transform.localRotation = rot;
		g.transform.localScale = scale;
		furnitures.transforms.Add (g.transform);
	}
	[SerializeField]
	public Furnitures furnitures;
	public void Load()
	{
		furnitures = JsonUtility.FromJson<Furnitures> (GetComponent<WriteRead>().Load());
		furnitures.transforms = new List<Transform> ();
		for (int i = 0; i < furnitures.indexes.Count; i++)
			Create (furnitures.indexes[i],furnitures.positions[i],furnitures.rotations[i],furnitures.scales[i]);
	}
	public void Save()
	{
		furnitures.GetAll ();
		GetComponent<WriteRead>().Save( JsonUtility.ToJson (furnitures));
	}
}

[System.Serializable]
public class Furnitures
{
	public List<Transform> transforms;
	public List<int> indexes;
	public List<Quaternion> rotations;
	public List<Vector3> positions;
	public List<Vector3> scales;
	public void GetAll ()
	{
		for (int i = 0; i < transforms.Count; i++) {
			rotations [i] = transforms [i].localRotation;
			positions [i] = transforms [i].localPosition;
			scales [i] = transforms [i].localScale;
		}
	}
}

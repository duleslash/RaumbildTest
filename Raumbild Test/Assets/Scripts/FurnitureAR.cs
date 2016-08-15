using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FurnitureAR : MonoBehaviour {
	
	public Kudan.AR.KudanTracker _kudanTracker;
	public void StartClicked()
	{
		// from the floor placer.
		Vector3 floorPosition;			// The current position in 3D space of the floor
		Quaternion floorOrientation;	// The current orientation of the floor in 3D space, relative to the device

		_kudanTracker.FloorPlaceGetPose(out floorPosition, out floorOrientation);	// Gets the position and orientation of the floor and assigns the referenced Vector3 and Quaternion those values
		_kudanTracker.ArbiTrackStart(floorPosition, floorOrientation);				// Starts markerless tracking based upon the given floor position and orientations
	}
	
	public void Reset () {
		SceneManager.LoadScene (0);
	}

}


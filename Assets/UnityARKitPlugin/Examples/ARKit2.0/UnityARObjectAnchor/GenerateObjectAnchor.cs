using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GenerateObjectAnchor : MonoBehaviour 
{

	[SerializeField]
	private ARReferenceObjectAsset referenceObjectAsset;

	[SerializeField]
	private GameObject prefabToGenerate;

	private GameObject objectAnchorGO;

	// Use this for initialization
	void Start () {
		UnityARSessionNativeInterface.ARObjectAnchorAddedEvent += AddObjectAnchor;
		UnityARSessionNativeInterface.ARObjectAnchorUpdatedEvent += UpdateObjectAnchor;
		UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveObjectAnchor;

	}

	void AddObjectAnchor(ARObjectAnchor arObjectAnchor)
	{
		Debug.Log ("object anchor added");
		if (arObjectAnchor.referenceObjectName == referenceObjectAsset.objectName) {
			Vector3 position = UnityARMatrixOps.GetPosition (arObjectAnchor.transform);
			Quaternion rotation = UnityARMatrixOps.GetRotation (arObjectAnchor.transform);

			objectAnchorGO = Instantiate<GameObject> (prefabToGenerate, position, rotation);
		}
	}

	void UpdateObjectAnchor(ARObjectAnchor arObjectAnchor)
	{
		Debug.Log ("object anchor added");
		if (arObjectAnchor.referenceObjectName == referenceObjectAsset.objectName) {
			objectAnchorGO.transform.position = UnityARMatrixOps.GetPosition (arObjectAnchor.transform);
			objectAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation (arObjectAnchor.transform);
		}

	}

	void RemoveObjectAnchor(ARImageAnchor arImageAnchor)
	{
		Debug.Log ("object anchor removed");
		if (objectAnchorGO) {
			GameObject.Destroy (objectAnchorGO);
		}

	}

	void OnDestroy()
	{
		UnityARSessionNativeInterface.ARObjectAnchorAddedEvent -= AddObjectAnchor;
		UnityARSessionNativeInterface.ARObjectAnchorUpdatedEvent -= UpdateObjectAnchor;
		UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveObjectAnchor;

	}

	// Update is called once per frame
	void Update () {

	}
}

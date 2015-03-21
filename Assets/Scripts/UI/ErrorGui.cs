using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorGui : MonoBehaviour {

	public Image energyImage = null;

	public Image ventilationImage = null;

	// Use this for initialization
	void Start () {
	
	}
	
	public bool EnergyError {
		get { return energyImage.enabled; }
		set { energyImage.enabled = value; energyImage.GetComponent<BlinkingIcon>().enabled = value; }
	}

	public bool VentilationError {
		get { return ventilationImage.enabled; }
		set { ventilationImage.enabled = value; ventilationImage.GetComponent<BlinkingIcon>().enabled = value; }
	}
}

using UnityEngine;
using System.Collections;

public class ClosePanel : MonoBehaviour {

	public void closePanel (GameObject panel) {
		panel.SetActive (!panel.activeSelf);
	}
}

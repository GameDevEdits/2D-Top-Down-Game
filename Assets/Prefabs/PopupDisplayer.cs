using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDisplayer : MonoBehaviour
{
    public GameObject popupText;
	public GameObject critPopupText;
	
	public void NoCrit()
	{
		popupText.SetActive(true);
		critPopupText.SetActive(false);
	}
	
	public void Crit()
	{
		popupText.SetActive(false);
		critPopupText.SetActive(true);
	}
}

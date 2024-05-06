using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDisplayer : MonoBehaviour
{
    public static GameObject popupText;
	public static GameObject critPopupText;
	
	public static void NoCrit()
	{
		popupText.SetActive(true);
		critPopupText.SetActive(false);
	}
	
	public static void Crit()
	{
		popupText.SetActive(false);
		critPopupText.SetActive(true);
	}
}

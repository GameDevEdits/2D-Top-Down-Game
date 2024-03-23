using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pfDamageTesting : MonoBehaviour
{
	[SerializeField]private RectTransform pfDamagePopup;
	
    private void Start()
	{
		Instantiate(pfDamagePopup, Vector3.zero, Quaternion.identity);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsDisplay : MonoBehaviour
{
    public static bool displayControls = false;
	[SerializeField]private TextMeshProUGUI controls;
	
	void Update()
	{
		//Will make the controls display whenever one of the Shift keys is held down
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			displayControls = true;
		}
		
		//Will remove the controls display when the shift key is released
		if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			displayControls = false;
		}
		
		if(displayControls)
		{
			controls.text = "WASD: Move Mouse: Aim LMB: Attack RMB: Block";
		}
		else
		{
			controls.text = "";
		}
	}
}

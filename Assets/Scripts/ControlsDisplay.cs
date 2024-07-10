using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsDisplay : MonoBehaviour
{
    public bool displayControls = true;
	[SerializeField]private TextMeshProUGUI controls;
	
	void Update()
	{
		//Will make the controls appear on screen if left or right Shift is pressed while they aren't visible (visible by default)
		if((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && !displayControls)
		{
			displayControls = true;
		}
		
		//Will remove the controls display when one of the the Control keys is pressed while the controls are visible
		if((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && displayControls)
		{
			displayControls = false;
		}
		
		if(displayControls)
		{
			controls.text = "WASD: Move"+System.Environment.NewLine+"Mouse: Aim"+System.Environment.NewLine+"LMB: Normal Attack"+System.Environment.NewLine+"RMB: Block"+System.Environment.NewLine+"Q: Heavy Attack"+System.Environment.NewLine+"Space: Dodge"+System.Environment.NewLine+"E: Interact"+System.Environment.NewLine+"Control: Remove Controls"+System.Environment.NewLine+"Shift: Display Controls";
		}
		else
		{
			controls.text = "";
		}
	}
}

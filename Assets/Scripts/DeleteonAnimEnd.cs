using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteonAnimEnd : MonoBehaviour
{
    public void DestroyParent()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}
}

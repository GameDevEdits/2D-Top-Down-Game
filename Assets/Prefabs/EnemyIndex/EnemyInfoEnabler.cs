using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyInfoEnabler : MonoBehaviour
{
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDisable;

    public Button button;

    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}

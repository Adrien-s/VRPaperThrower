using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TrigerZone>().OnEnterEvent.AddListener(InsideTrash);
    }

    public void InsideTrash(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

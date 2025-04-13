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
        // Score + reset
        ScoreManager.Instance.AddPoint();

        // Respawn
        Respawnable respawn = gameObject.GetComponent<Respawnable>();
        if (respawn != null)
        {
            respawn.Respawn();
        }
        else
        {
            // fallback : désactive si pas de script Respawnable
            gameObject.SetActive(false);
        }
    }

}

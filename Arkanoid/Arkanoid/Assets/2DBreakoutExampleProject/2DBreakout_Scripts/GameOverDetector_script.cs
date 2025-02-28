using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDetector_script : MonoBehaviour
{
    public GameObject gameOverPanel;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            Destroy(coll.gameObject);
            GameManager_script.gameOver = true; // Usa a variável estática
            gameOverPanel.SetActive(true);
        }
    }
}
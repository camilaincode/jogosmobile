using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    public Canvas GameOver;

private void Start()
    {
        GameOver.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void OnTriggerEnter2D(Collider2D outro) {

        if (outro.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            GameOver.gameObject.SetActive(true);
            return;
        } else
        {
            if (outro.gameObject.transform.parent)
                Destroy(outro.gameObject.transform.parent.gameObject);
            else
                Destroy(outro.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Platformer;

public class brancoScript : MonoBehaviour
{
        public SpriteRenderer tela_branca;
    void Start()
    {
                tela_branca = GameObject.Find("tela_branca").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if(other.gameObject.TryGetComponent<PlayerController>(out PlayerController pcs)) {
                pcs.tempoBranco += 9;
                tela_branca.enabled = true;
            }
            Destroy(this.gameObject);

    }

}

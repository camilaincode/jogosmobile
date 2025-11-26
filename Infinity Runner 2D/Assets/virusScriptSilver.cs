using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virusScriptSilver : MonoBehaviour
{
    public Animator animator;
    public GameObject placar;
    void Start()
    {
                animator = GetComponent<Animator>();
                placar = GameObject.Find("Placar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
            animator.SetBool("alive", false);
            placar.GetComponent<Placar>().placar += 50;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.SceneManagement;


public class LoadFase1 : MonoBehaviour


{
public string fase;
    public void loadFase() {
                SceneManager.LoadScene(fase); 

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneReload : MonoBehaviour
{
   public void ReloadScene(){
      SceneManager.LoadScene(1);

      Time.timeScale = 1f;
   }
}

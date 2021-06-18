using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
   public GameObject title;
   public GameObject player;
   public GameObject gameUI;

   private void Start()
   {
      player.SetActive(false);
      gameUI.SetActive(false);
      SoundManager.instance.bgSound.Stop();
   }

   public void StartBtn()
   {
      title.SetActive(false);
      player.SetActive(true);
      gameUI.SetActive(true);
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
      SoundManager.instance.bgSound.Play();
   }

   public void ExitBtn()
   {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
   }
}
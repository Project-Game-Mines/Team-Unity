using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   [SerializeField] private GameManager gameManager;
   
   [SerializeField] private List<MineButtonBehavior> mineButtons;

   

   private void isActive(GameManager gameManager)
   {
      gameManager.active = false;
   }
   
   //Chama as funçoes de resetar minas e icones para cada mina
   public void ResetMinesButtons()
   {
      foreach (var mine in mineButtons)
      {
         mine.ResetButtons();
         mine.ShowIconsEndGame();
      }
      StartCoroutine(WaitSeconds());
      
      
   }

   //Libera o grind, quando starta o game libera cada mina
   public void UnlockGridMines()
   {
      foreach (var mine in mineButtons)
      if (gameManager.active == true)
      {
         mine.animator.SetBool("Active", true);
         mine.active = true;
      }
   }

   
   private IEnumerator WaitSeconds()
   {
      yield return new WaitForSeconds(3);
      foreach (var mine in mineButtons)
      {
         mine.ResetButtonsIcons();
      }

      GameManager.minesPositions = null;
      
   }

  

   
   
}

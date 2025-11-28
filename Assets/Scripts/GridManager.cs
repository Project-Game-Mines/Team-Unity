using System;
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
   
   public void ResetMinesButtons()
   {
      foreach (var mine in mineButtons)
      {
         mine.ResetButtons();
      }
   }

   public void UnlockGridMines()
   {
      foreach (var mine in mineButtons)
      if (gameManager.active == true)
      {
         mine.animator.SetBool("Active", true);
         mine.active = true;
      }
   }
}

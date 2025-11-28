using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   private GameManager gameManager;

   private void Start()
   {
   }

   private void isActive(GameManager gameManager)
   {
      gameManager.active = false;
   }
}

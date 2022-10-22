using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public Player player;
   public ParticleSystem explosion;
   public float respawnTime = 3.0f;
   public float respawnReActiveTime = 3.0f;
   public int lives = 3;
   public int score = 0;

   public void AsteroidDestroyed(Asteroid asteroid)
   {
      explosion.transform.position = asteroid.transform.position;
      explosion.Play();
   }

   public void PlayerDied()
   {
      explosion.transform.position = player.transform.position;
      explosion.Play();
      
      lives--;

      if (lives <= 0)
      {
         GameOver();
      }

      else
      {
         Invoke(nameof(Respawn), respawnTime);
      }
   }

   private void Respawn()
   {
      player.transform.position = Vector3.zero;
      player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
      player.gameObject.SetActive(true);
     
      Invoke(nameof(TurnOnCollisions), respawnReActiveTime);
   }

   private void TurnOnCollisions()
   {
      player.gameObject.layer = LayerMask.NameToLayer("Player");
   }

   private void GameOver()
   {
      
   }
}

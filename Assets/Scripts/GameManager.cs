using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public Player player;
   public ParticleSystem explosion;
   public float respawnTime = 3.0f;
   [Header("Numerical")]
   public float respawnReActiveTime = 3.0f;
   public int lives = 3;
   public int score = 0;
   [SerializeField] private int winScore;
   [Header("UI")]
   public TMP_Text scoreText;
   public Image[] heartImages;
   public Sprite heartSprite;
   public Sprite brokenHeartSprite;
   public GameObject losePanel;
   public GameObject winPanel;
   public bool isGameOver = false;
   
   private void Start()
   {
      scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
      UpdateScoreText();
      UpdateLivesUI();
   }

   void UpdateScoreText()
   {
      scoreText.text = "Score: " + score.ToString();
   }

   void UpdateLivesUI()
   {
      for (int i = 0; i < heartImages.Length; i++)
      {
         if (i < lives)
         {
            heartImages[i].sprite = heartSprite;
         }
         else
         {
            heartImages[i].sprite = brokenHeartSprite;
         }
      }
   }
   
   public void AsteroidDestroyed(Asteroid asteroid)
   {
      explosion.transform.position = asteroid.transform.position;
      explosion.Play();

      if (asteroid.size < 0.75f)
      {
         score += 100;
      }
      else if (asteroid.size < 1.2f)
      {
         score += 50;
      }
      else
      {
         score += 25;
      }
      
      UpdateScoreText();

      if (score >= winScore && !isGameOver)
      {
         WinGame();
      }
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
         UpdateLivesUI();
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
      isGameOver = true;
      losePanel.SetActive(true);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
   }

   private void WinGame()
   {
      isGameOver = true;
      winPanel.SetActive(true);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
   }
}
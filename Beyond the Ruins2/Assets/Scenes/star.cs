using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class star : MonoBehaviour
{
    public Text starText;
    public AudioSource starSound;
    private int starcount = 0;
    public int requiredStar = 5;

 private void OnTriggerEnter(Collider other)
 {
    if(other.CompareTag("Star"))
    {
      starcount++;
      starText.text = "Star: " + starcount.ToString();
       // gameObject.SetActive(false);
       starSound.Play();
       Destroy(other.gameObject);

       if (starcount >= requiredStar)
       {
          SceneManager.LoadScene("Win");
       }
    }
 } 
 private void OnCollisionEnter(Collision other)
 {
   if (other.gameObject.CompareTag("obstacle"))
   {
      SceneManager.LoadScene("Lose");
   }
 }
}


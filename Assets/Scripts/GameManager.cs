using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject m_GameOverText;
    [SerializeField] private Text m_TimerText;
    [SerializeField] private Text m_AttemptsText;


    private Card m_LastCardOpen;

    private bool isCardOpen = false;
    private bool isClickable = true;
    private bool isGameOver = false;

    private int m_CardCount = 16;



    private void Update() {

        if (isGameOver) {

            // New game
            if (Input.GetKeyDown(KeyCode.Return)) {
                SceneManager.LoadScene(0);
            }

        } else {

            // Update timer
            m_TimerText.text = "Time: " + Mathf.RoundToInt(Time.timeSinceLevelLoad);

        }

    }

    // Invokes on card click
    public void OnCardClicked(GameObject card) {

        // Disable 3 and more card choose
        if (isClickable) {

            Card clickedCard = new Card(card);

            clickedCard.HideBackside();

            if (isCardOpen) {

                StartCoroutine("CompareCards", clickedCard);

            } else {

                isCardOpen = true;
                m_LastCardOpen = clickedCard;

            } 

        }

        // Update attempts count
        m_AttemptsText.text = "Attempts: " + (int.Parse(m_AttemptsText.text.Substring(10)) + 1);

    }

    // Comparing two cards
    private IEnumerator CompareCards(Card clickedCard) {

        isClickable = false;

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);

        if (clickedCard.Image.sprite == m_LastCardOpen.Image.sprite) {

            // Hide same cards
            clickedCard.HideImage();
            m_LastCardOpen.HideImage();
            m_CardCount -= 2;

        } else {

            // Show backside of those cards
            clickedCard.ShowBackside();
            m_LastCardOpen.ShowBackside();

        }

        isCardOpen = false;
        isClickable = true;

        CheckEnd();

    }

    private void CheckEnd() {

        // If there is no cards - Game Over
        if (m_CardCount == 0) {
            m_GameOverText.SetActive(true);
            isGameOver = true;
        }

    }
}

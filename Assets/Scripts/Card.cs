using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card {

    private Image image;
    private Image backside;

    public Image Image {
        get {
            return image;
        }
    }
    public Image Backside {
        get {
            return backside;
        }
    }


    public Card(GameObject card) {

        image = card.transform.GetChild(0).GetComponent<Image>();
        backside = card.transform.GetChild(1).GetComponent<Image>();

    }

    public void HideImage() {

        image.enabled = false;

    }

    public void HideBackside() {

        backside.enabled = false;

    }

    public void ShowBackside() {

        backside.enabled = true;

    }
}

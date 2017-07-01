using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageFiller : MonoBehaviour {

    [SerializeField] private List<Sprite> m_Sprites;

	// Use this for initialization
	void Start () {

        List<Image> childImages = new List<Image>();

        foreach (Transform child in transform.GetComponentsInChildren<Transform>()) {

            if (child.name == "Image") {

                childImages.Add(child.GetComponent<Image>());

            }

        }

        m_Sprites.AddRange(m_Sprites);

        SetImages(childImages);

    }

    private void SetImages(List<Image> childButtons) {

        foreach (var image in childButtons) {

            Sprite sprite = m_Sprites[Random.Range(0, m_Sprites.Count)];

            m_Sprites.Remove(sprite);

            image.sprite = sprite;


        }

    }

}

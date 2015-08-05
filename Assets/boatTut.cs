using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class boatTut : MonoBehaviour {

	public Image panel1, panel2, button1, button2, button3, button4;
	public Text text1, text2;

	public float fadeTime, fadeAmt;

	public bool fading;

	void OnEnable(){
		Invoke ("Fade", fadeTime);
	}

	void Fade(){
		print ("fade called");
		fading = true;

	}

	void FadeImage(Image imgToFade){
		imgToFade.color = new Color (imgToFade.color.r, imgToFade.color.g, imgToFade.color.b, imgToFade.color.a - fadeAmt);
	}

	void FadeText(Text textToFade){
		textToFade.color = new Color (textToFade.color.r, textToFade.color.g, textToFade.color.b, textToFade.color.a - fadeAmt);
	}

	void Update(){
		if (fading) {
			FadeImage(panel1);
			FadeImage(panel2);
			FadeImage(button1);
			FadeImage(button2);
			FadeImage(button3);
			FadeImage(button4);

			FadeText (text1);
			FadeText (text2);
		
			if (panel1.color.a <= 0){
				Destroy (gameObject);
			}
		}
	}

}


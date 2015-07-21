using UnityEngine;
using System.Collections;

public class DialoguerGUI : MonoBehaviour {

	private bool _showing;

	private string _text;

	void Start(){
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
	}

	

	void onStarted () {
		_showing = true;
	}

	void onEnded () {
		_showing = false;	
	}

	void onTextPhase(DialoguerTextData data){
		_text = data.text;
	}
}

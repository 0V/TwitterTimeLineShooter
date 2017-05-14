using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TestModalWindow : MonoBehaviour
  {
	private ModalPanel ModalPanel;           //reference to the ModalPanel Class
	private DisplayManager DisplayManager;   //reference to the DisplayManager Class

	public Sprite ErrorIcon;                 //Your error icon
	public Sprite InformationIcon;           //Your information icon
	public Sprite ProgramIcon;               //Your Company Logo or Program Icon
	public Sprite WarningIcon;               //Your warning icon
	public Sprite QuestionIcon;              //Your question icon

	void Awake()
	  {
		ModalPanel = ModalPanel.Instance();         //Instantiate the panel
		DisplayManager = DisplayManager.Instance(); //Instantiate the Display Manager
	  }
	//Test function:  Pop up the Modal Window with Yes, No, and Cancel buttons.
	public void TestYNC()
	  {
		Sprite icon = null;
		ModalPanel.MessageBox(icon, "Test Yes No Cancel", "Would you like a poke in the eye?\nHow about with a sharp stick?", TestYesFunction, TestNoFunction, TestCancelFunction, TestOkFunction, false, "YesNoCancel");
	  }
	//Test function:  Pop up the Modal Window with Yes, No, and Cancel buttons and an Icon.
	public void TestYNCI()
	  {
		Sprite icon = ProgramIcon;
		ModalPanel.MessageBox(icon, "Test Yes No Cancel Icon", "Do you like this icon?", TestYesFunction, TestNoFunction, TestCancelFunction, TestOkFunction, true, "YesNoCancel");
	  }
	//Test function:  Pop up the Modal Window with Yes and No buttons.
	public void TestYN()
	 {
		Sprite icon = null;
		ModalPanel.MessageBox(icon, "Test Yes No", "Answer 'Yes' or 'No':", TestYesFunction, TestNoFunction, TestCancelFunction, TestOkFunction, false, "YesNo");
	  }
	//Test function:  Pop up the Modal Window with an Ok button.
	public void TestOk()
	  {
		Sprite icon = null;
		ModalPanel.MessageBox(icon, "Test Ok", "Please hit ok.", TestYesFunction, TestNoFunction, TestCancelFunction, TestOkFunction, false, "Ok");
	  }
	//Test function:  Pop up the Modal Window with an Ok button and an Icon.
	public void TestOkIcon()
	  {
		Sprite icon = InformationIcon;
		ModalPanel.MessageBox(icon, "Test OK Icon", "Press Ok.", TestYesFunction, TestNoFunction, TestCancelFunction, TestOkFunction, true, "Ok");
	  }
	//Test function:  Do something if the "Yes" button is clicked.
	void TestYesFunction()
	  {
		DisplayManager.DisplayMessage("Heck yeah! Yup!");
	  }
	//Test function:  Do something if the "No" button is clicked.
	void TestNoFunction()
	  {
		DisplayManager.DisplayMessage("No way, José!");
	  }
	//Test function:  Do something if the "Cancel" button is clicked.
	void TestCancelFunction()
	  {
		DisplayManager.DisplayMessage("I give up!");
	  }
	//Test function:  Do something if the "Ok" button is clicked.
	void TestOkFunction()
	  {
		DisplayManager.DisplayMessage("Ok!");
	  }
  }
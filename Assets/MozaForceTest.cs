using UnityEngine;
using mozaAPI;
using TMPro;

public class MozaForceTest : MonoBehaviour
{
    public GEAR gearData;
    public TextMeshProUGUI debugText;
    
    private bool[] buttonStates = new bool[20];
    private int lastActiveButton = -1;
    
    void Update()
    {
        bool stateChanged = false;
        
        for(int i = 0; i < 20; i++)
        {
            bool currentState = Input.GetKeyDown((KeyCode)((int)KeyCode.Joystick1Button0 + i));
            if(currentState)
            {
                if(lastActiveButton != -1 && lastActiveButton != i)
                {
                    buttonStates[lastActiveButton] = false;
                }
                
                buttonStates[i] = true;
                lastActiveButton = i;
                Debug.Log($"Button {i}: {buttonStates[i]}");
                stateChanged = true;
            }
        }
        
        if(stateChanged && debugText != null)
        {
            string log = "";
            for(int i = 0; i < 20; i++)
            {
                if(buttonStates[i])
                {
                    log += $"Button {i}: <color=#00FF00>true</color>\n";
                }
                else
                {
                    log += $"Button {i}: false\n";
                }
            }
            debugText.text = log;
        }
    }
}
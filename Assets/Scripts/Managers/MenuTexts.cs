using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuHoverTexts", menuName = "MenuHover", order = 1)]
public class MenuTexts : ScriptableObject {
    public string IntroText;
    public string SinglePlayerHover;
    public string MultiPlayerHover;
    public string OptionsHover;
    public string ExitHover;
}

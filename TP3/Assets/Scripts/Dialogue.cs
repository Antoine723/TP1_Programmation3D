using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private List<string> liste = new List<string>(); //On déclare une nouvelle liste qui va permettre
    //de saisir les différents textes pour chacun des personnages
    public List<string> Liste => liste; //On récupère cette liste
}

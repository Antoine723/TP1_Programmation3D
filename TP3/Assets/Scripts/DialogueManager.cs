using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private TextMeshProUGUI first;
    [SerializeField] private TextMeshProUGUI second;
    void Start()
    {
        if (dialogue.Liste.Count >= 2)
        {
            first.text = dialogue.Liste[0]; //On commence par afficher les 2 premières phrases
            second.text = dialogue.Liste[1];
        }
        
    }
    

    private int i = 2; //On déclare notre index comme étant égal à 2 pour itérer
    //à partir du dernier texte affiché
    
    public void OnClick() //Fonction qui va être appelée à chaque clic sur le bouton
    {
        if (i < dialogue.Liste.Count) //Si on n'a pas encore parcouru tous les textes du dialogue
        {
            first.text=dialogue.Liste[i]; //On affiche pour le 1er perso le texte correspondant
            if (i < dialogue.Liste.Count - 1) //Si il y a une réponse du 2ème perso on l'affiche
            {
                second.text=dialogue.Liste[i+1];
            }
            else
            {
                second.text = ""; //Sinon on n'affiche pas de texte pour le 2ème perso
                
            }
            i+=2; //Comme il y a deux personnages, on incrémente de 2 l'index à chaque fois
        }
        else //Si on a parcouru tous les textes on recommence
        {
            first.text=dialogue.Liste[0];
            second.text=dialogue.Liste[1];
            i = 2;
            
        }
    }
}

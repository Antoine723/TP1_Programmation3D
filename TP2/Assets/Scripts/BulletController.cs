using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private String[] targetComp=new String[] {"Head","Right Arm","Right Leg","Left Arm","Left Leg"}; //Liste qui contient les parties du corps à détruire si on les touche

    private void Start()
    {
        Coroutine coroutine =StartCoroutine(DestroyIn3Seconds()); //Va relancer la fonction en argument en permanence (routine) donc ici,
    }

    private IEnumerator DestroyIn3Seconds() //Pour une coroutine : toujours prendre en argument un énumérateur
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetComp.Contains(other.name)) //Si on touche une partie du corps autre que le body, on détruit cette partie du corps
        {
            Destroy(other.gameObject);
        }
        else if (other.name == "Body") //Si on touche le body on détruit la cible complètement
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
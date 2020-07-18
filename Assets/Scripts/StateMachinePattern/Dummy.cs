using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _damageText = null;
    [SerializeField] private TextMeshProUGUI _debuffText = null;
    [SerializeField] private Animation _damageAnimation = null;
    [SerializeField] private Animation _debuffAnimation = null;

    public Vector3 Position => transform.position;
   
    public void Hit(int damage, bool isStunned, bool isSlowed, bool isPoisoned)
    {
        _damageText.text = "+" + damage;

        string debuffText;

        if (isStunned) debuffText = "Stunned";
        else if (isSlowed) debuffText = "Slowed";
        else debuffText = "Poisoned";

        _debuffText.text = debuffText;

        _damageAnimation.Play();
        _debuffAnimation.Play();
    }
}

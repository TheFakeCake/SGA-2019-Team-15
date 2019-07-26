using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GUI : MonoBehaviour
{
    public Color couleurVisMorte = Color.grey;

    private ControleJoueur subMarine;
    private Image vis1;
    private Image vis2;
    private Image vis3;
    private int uiHp;

    // Start is called before the first frame update
    void Start()
    {
        vis1 = transform.Find("Vis 1").gameObject.GetComponent<Image>();
        vis2 = transform.Find("Vis 2").gameObject.GetComponent<Image>();
        vis3 = transform.Find("Vis 3").gameObject.GetComponent<Image>();
        subMarine = GameObject.Find("Sous-marin joueur").GetComponent<ControleJoueur>();
        uiHp = subMarine.getHp();
    }

    // Update is called once per frame
    void Update()
    {
        // Met à jour les vis en fonction des HP du sous-marin
        if (subMarine.getHp() != uiHp) {
            uiHp = subMarine.getHp();

            vis1.color = (uiHp > 0 ? Color.white : couleurVisMorte);
            vis2.color = (uiHp > 1 ? Color.white : couleurVisMorte);
            vis3.color = (uiHp > 2 ? Color.white : couleurVisMorte);
        }
    }
}

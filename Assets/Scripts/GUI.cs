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
    private Image torpille1;
    private Image torpille2;
    private Image torpille3;
    private Image torpille4;
    private Image torpille5;
    private int uiHp;
    private int uiAmmo;

    // Start is called before the first frame update
    void Start()
    {
        vis1 = transform.Find("Vis 1").gameObject.GetComponent<Image>();
        vis2 = transform.Find("Vis 2").gameObject.GetComponent<Image>();
        vis3 = transform.Find("Vis 3").gameObject.GetComponent<Image>();
        torpille1 = transform.Find("Torpille 1").gameObject.GetComponent<Image>();
        torpille2 = transform.Find("Torpille 2").gameObject.GetComponent<Image>();
        torpille3 = transform.Find("Torpille 3").gameObject.GetComponent<Image>();
        torpille4 = transform.Find("Torpille 4").gameObject.GetComponent<Image>();
        torpille5 = transform.Find("Torpille 5").gameObject.GetComponent<Image>();
        subMarine = GameObject.Find("Sous-marin joueur").GetComponent<ControleJoueur>();
        uiHp = subMarine.getHp();
        uiAmmo = subMarine.getAmmo();

        updateAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        updateVis();
        updateAmmo();
    }

    private void updateVis()
    {
        // Met à jour les vis en fonction des HP du sous-marin
        if (subMarine.getHp() != uiHp) {
            uiHp = subMarine.getHp();

            vis1.color = (uiHp > 0 ? Color.white : couleurVisMorte);
            vis2.color = (uiHp > 1 ? Color.white : couleurVisMorte);
            vis3.color = (uiHp > 2 ? Color.white : couleurVisMorte);
        }
    }

    private void updateAmmo()
    {
        // Met à jour les torpilles en fonction des munitions du sous-marin
        if (subMarine.getAmmo() != uiAmmo) {
            uiAmmo = subMarine.getAmmo();

            torpille1.gameObject.SetActive(uiAmmo > 0);
            torpille2.gameObject.SetActive(uiAmmo > 1);
            torpille3.gameObject.SetActive(uiAmmo > 2);
            torpille4.gameObject.SetActive(uiAmmo > 3);
            torpille5.gameObject.SetActive(uiAmmo > 4);
        }
    }
}

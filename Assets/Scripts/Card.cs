using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//dotween 사용
using DG.Tweening;


public class Card : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer card;

    [SerializeField]
    SpriteRenderer character;

    [SerializeField]
    TMP_Text card_name_tmp;

    [SerializeField]
    TMP_Text card_atk_tmp;

    [SerializeField]
    TMP_Text card_hp_tmp;

    [SerializeField]
    Sprite cardFront;

    [SerializeField]
    Sprite cardBack;

    public Item item;
    bool isFront;
    public PRS originPRS;

    

    public void Setup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if (this.isFront)
        {
            character.sprite = this.item.sprite;
            card_name_tmp.text = this.item.name;
            card_atk_tmp.text = this.item.attack.ToString();
            card_hp_tmp.text = this.item.health.ToString();
        }
        else
        {
            card.sprite = cardBack;
            card_name_tmp.text = "";
            card_atk_tmp.text = "";
            card_hp_tmp.text = "";
        }
    }

    public void MoveTransform(PRS prs,bool useDotween,float dotweenTime=0){
        if(useDotween){
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else{
            transform.position=prs.pos;
            transform.rotation=prs.rot;
            transform.localScale=prs.scale;
        }
    }
}

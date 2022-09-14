using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class fortest : MonoBehaviour
{
    public string
        tokenId =
            "67653935963194648804561750712248565766761565309523376547942392229125200805889";

    public GameObject CanYouSeeMe;

    async void Start()
    {
        string chain = "polygon";
        string network = "mainnet";
        string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963";
        string account = PlayerPrefs.GetString("Account");

        BigInteger balanceOf =
            await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        print (balanceOf);

        if (balanceOf > 0)
        {
            CanYouSeeMe.SetActive(true);
        }
        else
        {
            CanYouSeeMe.SetActive(false);
        }
    }
}

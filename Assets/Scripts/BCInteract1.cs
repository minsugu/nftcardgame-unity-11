using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

//Used by metadata class for storing attributes
public class Attributes1
{
    //The type or name of a given trait
    public string trait_type;

    //The value associated with the trait_type
    public string value;
}

//Used for storing NFT metadata from standard NFT json files
public class Metadata1
{
    //List storing attributes of the NFT
    public List<Attributes1> attributes { get; set; }

    //Description of the NFT
    public string description { get; set; }

    //An external_url related to the NFT (often a website)
    public string external_url { get; set; }

    //image stores the NFTs URI for image NFTs
    public string image { get; set; }

    //Name of the NFT
    public string name { get; set; }
}

//Interacting with blockchain
public class BCInteract1 : MonoBehaviour
{
    //The chain to interact with, using Polygon here
    public string chain = "polygon";

    //The network to interact with (mainnet, testnet)
    public string network = "testnet";

    //Contract to interact with, contract below is "Project: Pigeon Smart Contract"
    public string contract = "0xfF9FEFF81605711D21f9C02cB1C7cE6735a3d395";

    //Token ID to pull from contract
    public string tokenId = "13";

    //Used for storing metadata
    Metadata metadata;

    private void Start()
    {
        //Starts async function to get the NFT image
       GetNFTImage();
    }
    public void GetImg(){
        GetNFTImage();
    }
    private async void GetNFTImage()
    {
        //Interacts with the Blockchain to find the URI related to that specific token
        string URI = await ERC721.URI(chain, network, contract, tokenId);

        //Perform webrequest to get JSON file from URI
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URI))
        {
            //Sends webrequest
            await webRequest.SendWebRequest();

            //Gets text from webrequest
            string metadataString = webRequest.downloadHandler.text;

            //Converts the metadata string to the Metadata object
            metadata = JsonConvert.DeserializeObject<Metadata>(metadataString);
        }

        //Performs another web request to collect the image related to the NFT
        using (
            UnityWebRequest webRequest =
                UnityWebRequestTexture.GetTexture(metadata.image)
        )
        {
            //Sends webrequest
            await webRequest.SendWebRequest();

            Texture2D webTexture =
                ((DownloadHandlerTexture) webRequest.downloadHandler).texture as
                Texture2D;
            Sprite webSprite = SpriteFromTexture2D(webTexture);
            GetComponent<SpriteRenderer>().sprite = webSprite;
        }
         Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite
            .Create(texture,
            new Rect(0.0f, 0.0f, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            100.0f);
    }
    }
}

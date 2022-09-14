using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ImportNFTIPFS : MonoBehaviour
{
    int _rotationSpeed = 6;

    [Serializable]
    public class Attribute
    {
        public string trait_type { get; set; }

        public string value { get; set; }
    }

    [Serializable]
    public class Response
    {
        public string image { get; set; }

        public string name { get; set; }


        public List<Attribute> attributes { get; set; }
    }

    public GameObject gameObject1;

    async void Start()
    {
        string chain = "polygon";
        string network = "testnet";

        // BAYC contract address
        string contract = "0xfF9FEFF81605711D21f9C02cB1C7cE6735a3d395";
        string tokenId = "13";

        // fetch uri from chain
        string uri = await ERC721.URI(chain, network, contract, tokenId);
        print("uri: " + uri);

        if (uri.StartsWith("ipfs://"))
        {
            uri = uri.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
        print("URI: " + uri);

        // fetch json from uri
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        await webRequest.SendWebRequest();
        Response data =
            JsonConvert
                .DeserializeObject<Response>(System
                    .Text
                    .Encoding
                    .UTF8
                    .GetString(webRequest.downloadHandler.data));
        print("Data: " + data.image);
        print("Name:" + data.name);

        // parse json to get image uri
        string imageUri = data.image;
        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
        print("imageUri: " + imageUri);
        print("Attibutes: " + data.attributes[0].trait_type);
        print("Attibutes: " + data.attributes[0].value);
        print("Attibutes: " + data.attributes[1].trait_type);
        print("Attibutes: " + data.attributes[2].trait_type);

        // fetch image and display in game
        UnityWebRequest textureRequest =
            UnityWebRequestTexture.GetTexture(imageUri);
        await textureRequest.SendWebRequest();
        this.gameObject.GetComponent<Renderer>().material.mainTexture =
            ((DownloadHandlerTexture) textureRequest.downloadHandler).texture;
    }

    public void FixedUpdate()
    {
        // be sure to capitalize Rotate or you'll get errors
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, -2);
    }
}

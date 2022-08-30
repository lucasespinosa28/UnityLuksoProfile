using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProfileMetadata;

public class JSONURL
{

    public static (ProfileMetadata, string) EncodeJSONURL(
        string _name,
        string _description,
        string ipfsProfile,
        string hashProfile,
        string ipfsBackground,
        string hashBackground)
    {
        var metadataJson = new ProfileMetadata();
        var profileImage = new Profileimage
        {
            width = 640,
            height = 609,
            hashFunction = "keccak256(bytes)",
            hash = $"{hashProfile}",
            url = $"ipfs://{ipfsProfile}"
        };
        var backgroundimage = new Backgroundimage
        {
            width = 1024,
            height = 576,
            hashFunction = "keccak256(bytes)",
            hash = $"{hashBackground}",
            url = $"ipfs://{ipfsBackground}"
        };

        var lsp3profile = new Lsp3profile
        {
            name = _name,
            description = _description,
            links = null,
            tags = null,
            profileImage = new Profileimage[]
        {
    profileImage
        },
            backgroundImage = new Backgroundimage[]
        {
    backgroundimage
        }
        };
        metadataJson.LSP3Profile = lsp3profile;

    var Encode = new ABIEncode();
        var hashFunction = Encode.GetSha3ABIEncodedPacked("keccak256(utf8)")
            .ToHex()
            .Substring(0, 8);
        //var metadataJson = getJsonData();
        string jsonString = JsonUtility.ToJson(metadataJson);
        Debug.Log($"hashFunction: {hashFunction}");
        var hash = Encode.GetSha3ABIEncodedPacked(jsonString)
            .ToHex();
        Debug.Log($"hash: {hash}");
        var url = Encode.GetABIEncodedPacked($"ipfs://{PlayerPrefs.GetString("IpfsProfile")}")
            .ToHex();
        Debug.Log($"url: {url}");
        Debug.Log($"concat: {string.Concat(hashFunction, hash, url)}");
        return (metadataJson, string.Concat(hashFunction, hash, url));
    }

    //public static ProfileMetadata getJsonData(
    //    string _name, 
    //    string _description,
    //    string ipfsProfile,
    //    string hashProfile,
    //    string ipfsBackground,
    //    string hashBackground)
    //{
    //    var metadataJson = new ProfileMetadata();
    //    var profileImage = new Profileimage
    //    {
    //        width = 640,
    //        height = 609,
    //        hashFunction = "keccak256(bytes)",
    //        hash = $"{hashProfile}",
    //        url = $"ipfs://{ipfsProfile}"
    //    };
    //    var backgroundimage = new Backgroundimage
    //    {
    //        width = 1024,
    //        height = 576,
    //        hashFunction = "keccak256(bytes)",
    //        hash = $"{hashBackground}",
    //        url = $"ipfs://{ipfsBackground}"
    //    };

    //    var lsp3profile = new Lsp3profile
    //    {
    //        name = _name,
    //        description = _description,
    //        links = null,
    //        tags = null,
    //        profileImage = new Profileimage[]
    //    {
    //profileImage
    //    },
    //        backgroundImage = new Backgroundimage[]
    //    {
    //backgroundimage
    //    }
    //    };
    //    metadataJson.LSP3Profile = lsp3profile;
    //    return metadataJson;
    //}
}

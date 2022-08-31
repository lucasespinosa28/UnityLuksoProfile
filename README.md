# UnityLuksoProfile
The project allows you to embed a web3 wallet in unity games and create a Lukso profile.

[Demo](https://youtu.be/gpN7k9XqleM)

[Unity Package](https://github.com/lucasespinosa28/UnityLuksoProfile/releases/tag/0.0.1-alpha)

project uses [pinata](https://www.pinata.cloud/) to upload file to ipfs, in future will have option to connect to custom ipfs node, where is token add pinanta JWT token

![Captura de tela 2022-08-30 210733](https://user-images.githubusercontent.com/52639395/187564819-f7b47be9-4d14-4691-8995-220be3b1c817.png)
# Screenshots
![Captura de tela 2022-08-30 204727](https://user-images.githubusercontent.com/52639395/187564024-922fe693-b5bb-413e-bf4a-22964a291d6d.png)
![Captura de tela 2022-08-30 204341](https://user-images.githubusercontent.com/52639395/187564025-c71a9757-6142-4150-904e-708b46557e08.png)
![Captura de tela 2022-08-30 204616](https://user-images.githubusercontent.com/52639395/187564028-d176a6bf-e41c-4f45-abfd-202a3a4538ce.png)

### after the user clicks on the import profile, it will be possible to get the profile data using this code:
```csharp
PlayerPrefs.SetString("LSP3Profile.name");
PlayerPrefs.GetString("LSP3Profile.description");
PlayerPrefs.GetString("LSP3Profile.backgroundImage");
PlayerPrefs.GetString("LSP3Profile.profileImage");
```

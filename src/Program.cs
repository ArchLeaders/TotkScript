using CsOead;
using Native.IO.Handles;
using Native.IO.Services;

NativeLibraryManager
    .RegisterPath("D:\\Bin\\native", out bool isCommonLoaded)
    .Register(new OeadLibrary(), out bool isOeadSuccess);

Console.WriteLine(isCommonLoaded);
Console.WriteLine(isOeadSuccess);

const string path = ".\\Armor_001_Head.pack";

using FileStream fs = File.OpenRead(path);
byte[] buffer = new byte[fs.Length];
fs.Read(buffer);

Sarc sarc = Sarc.FromBinary(buffer);
ReadOnlySpan<byte> data = sarc["Component/ArmorParam/Armor_001_Head.game__component__ArmorParam.bgyml"];

Byml byml = Byml.FromBinary(data);
BymlHash hash = byml.GetHash();
hash["BaseDefense"] = 6;

DataMarshal outputData = byml.ToBinary(Endianness.Little, 7);
sarc["Component/ArmorParam/Armor_001_Head.game__component__ArmorParam.bgyml"] = outputData;

DataMarshal outputSarc = sarc.ToBinary(Endianness.Little);

const string outputPath = ".\\Armor_001_Head.output.pack";
using FileStream output = File.Create(outputPath);
output.Write(outputSarc.AsSpan());
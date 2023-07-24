# Modding TotK with C# Tutorial

This is the written tutorial for a [video tutorial I made](https://youtu.be/pKMcCp1WjoU).

---

- [Modding TotK with C# Tutorial](#modding-totk-with-c-tutorial)
- [Tools \& Setup](#tools--setup)
  - [IDE or Text Editor](#ide-or-text-editor)
  - [Other Software](#other-software)
  - [Adding MinGW and Ninja to the PATH](#adding-mingw-and-ninja-to-the-path)
    - [Windows](#windows)
- [NX-Editor Setup](#nx-editor-setup)
- [Creating the C# Project](#creating-the-c-project)
- [Setting up libraries (Part 1, git)](#setting-up-libraries-part-1-git)
  - [Initializing a Repository](#initializing-a-repository)
- [Setting up libraries (Part 1, cs-oead)](#setting-up-libraries-part-1-cs-oead)
  - [Cloning cs-oead](#cloning-cs-oead)
  - [Adding the project reference](#adding-the-project-reference)
- [Add Project Reference \& Load CsOead](#add-project-reference--load-csoead)
  - [Building CsOead and Native.IO (C++)](#building-csoead-and-nativeio-c)
    - [Building Native.IO](#building-nativeio)
  - [Add Projects Reference](#add-projects-reference)
  - [Loading CsOead](#loading-csoead)
- [Copying \& Decompressing the Game Files](#copying--decompressing-the-game-files)
  - [Decompressing](#decompressing)
  - [Plan your Edits](#plan-your-edits)
- [Loading the File in Memory](#loading-the-file-in-memory)
  - [Alternative Method](#alternative-method)
- [Parsing a SARC](#parsing-a-sarc)
- [Parsing a BYML](#parsing-a-byml)
- [Saving the Modified Files](#saving-the-modified-files)
- [Checking the Results](#checking-the-results)
- [Summary](#summary)

# Tools & Setup
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=13s)

## IDE or Text Editor

The first step is to download and install all the required software to do this.

You'll need an IDE or text editor, I would recommend using **Visual Studio** if you're on Windows or MacOS, and **Visual Studio Code** *(yes they're different)* on Linux.

- [Visual Studio](https://visualstudio.microsoft.com/)
- [Visual Studio Code](https://code.visualstudio.com/)

Both of these are installed with a relatively straightforward installer.

## Other Software

The next couple things you'll need are the [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), the [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022), [MinGW](https://github.com/brechtsanders/winlibs_mingw/releases/tag/13.1.0-16.0.5-11.0.0-ucrt-r5), [Ninja](https://ninja-build.org/), [CMake](https://cmake.org/), and (optionally) [NX-Editor](https://nx-editor.github.io/) for viewing the game files before scripting edits.

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022)
- [MinGW](https://github.com/brechtsanders/winlibs_mingw/releases/tag/13.1.0-16.0.5-11.0.0-ucrt-r5)
- [Ninja](https://ninja-build.org/)
- [CMake](https://cmake.org/)
- [NX-Editor](https://nx-editor.github.io/)

For the [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), the [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022) and [CMake](https://cmake.org/), just run the downloaded setup exe and follow the steps to install.

As for [MinGW](https://github.com/brechtsanders/winlibs_mingw/releases/tag/13.1.0-16.0.5-11.0.0-ucrt-r5) and [Ninja](https://ninja-build.org/), these are both installed by extracting the zip archives and copying the contents to a folder of your choice (you'll need to [add these to PATH]() later, so keep in mind where you put it).

## Adding MinGW and Ninja to the PATH
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=350s)

For the commands used in this tutorial to work, you'll need to add MinGW and Ninja to the system PATH.

*(Likely not required for Linux if you used a app manager)*

### Windows

In the Windows search bar, look for the control panel application called `Edit the system environment variables` and open the application. Once that window loads, click the button to the bottom right labelled `Environment Variables...`

Under the **System variables**, scroll down until you find a key labelled `Path`; double-click the entry to edit it.

Finally, click the button on the top right labelled `New` and enter the path to the Ninja folder *(folder containing `ninja.exe`)* in the new entry. Do the same for the `MinGW` path, **and make sure to append `\bin` to the end of the path**.

# NX-Editor Setup
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=305s)

To set up NX-Editor, simply run the downloaded `exe` and provide your game path on the settings page.

The field will turn green if the game path is valid. If it does not, make sure your dump is valid and you've directed it to a folder containing the game folders *(not a folder containing a `romfs` or title id folder)*.

<img src="assets/game_dump.png" width=500>

# Creating the C# Project
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=427s)

Now that everything is properly set up, we can begin by creating a C# solution and project.

This can be done on all platforms using the dotnet SDK command-line tools.

1. Create a new folder on your computer and name it your project name (e.g. `TotkScript`)
2. In that folder, open a new terminal instance and type the following commands
   1. **Create a new solution:** `dotnet new sln -n TotkScript` *(replace `TotkScript` with your solution name)*
   2. **Create a new console application:** `dotnet new console -n TotkScript -o src` *(replace `TotkScript` with your project name; `-o` is an optional output folder for the csproj file)*
   3. **Add the project to your solution:** `dotnet sln add src/TotkScript.csproj` *(replace `src/TotkScript` with the relative path to your project file)*
3. Open the project/solution with your IDE or text editor of choice
4. Build and run the application to make sure everything works correctly

# Setting up libraries (Part 1, git)
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=617s)

Before moving forward, you'll need to install another piece of software, [git](https://git-scm.com/). This is a popular version control system used in modern development, it will allow us to clone [cs-oead](https://github.com/EPD-Libraries/cs-oead) into our project without re-publishing the code.

To install git, download the installer for your platform from [git-scm](https://git-scm.com/) and run it. In the setup wizard, click next until you reach the **Choosing the default editor used by git** page. On this page, use the dropdown to select your editor. After that, you can click next and leave everything at the default until you reach the final page where you can click **Install**.

## Initializing a Repository

Once [git](https://git-scm.com/) is installed, you'll want to initialize your repository. This is done differently depending on your IDE/editor, but for the simplest setup, you can just run `git init` in your project folder.

# Setting up libraries (Part 1, cs-oead)

## Cloning cs-oead

Now that [git](https://git-scm.com/) is installed (and a repository has been [initialized](#initializing-a-repository)), we can add [cs-oead](https://github.com/EPD-Libraries/cs-oead) as a **submodule** with the following commands.

1. **Add the Submodule:** `git submodule add https://github.com/EPD-Libraries/cs-oead lib/cs-oead` *(replace `lib/cs-oead` with your desired output folder for [cs-oead](https://github.com/EPD-Libraries/cs-oead))*
2. **Recursively Clone all Submodules:** `git submodule update --init --recursive`

## Adding the project reference

Once cloning has completed, you'll need to add the `CsOead` and `Native.IO` projects to your solution using these commands.

1. **Add CsOead:** `dotnet sln add lib/cs-oead/src/CsOead.csproj -s Libraries` *(replace `lib/cs-oead` with the clone path used in the previous step; `-s` optionally adds a folder to the solution for a cleaner project)*
2. **Add Native.IO:** `dotnet sln add lib/cs-oead/lib/Native.IO/src/Native.IO.csproj -s Libraries` *(replace `lib/cs-oead` with the clone path used in the previous step; `-s` optionally adds a folder to the solution for a cleaner project)*

# Add Project Reference & Load CsOead
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=946s)

## Building CsOead and Native.IO (C++)

From your root project folder, navigate to wherever you cloned the [cs-oead](https://github.com/EPD-Libraries/cs-oead) submodule (e.g. `lib/cs-oead`). From there, open a terminal in the `native` folder and run these commands.

```
cmake --no-warn-unused-cli -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE -DCMAKE_BUILD_TYPE:STRING=Release -B ./build -G "Ninja"
cmake --build ./build --config Release --target all -j 4
```

**Note:** If you're on Linux or MacOS, append `linux`/`macos` to the build path.

### Building Native.IO

From the [cs-oead](https://github.com/EPD-Libraries/cs-oead) directory, navigate to `lib/Native.IO/native` and run the above commands in a new terminal there. 

## Add Projects Reference

In your IDE/editor, open the console `csproj` file as `XML`, and in the `Project` tag (underneath `PropertyGroup`) add the following code:

```xml
<ItemGroup>
  <ProjectReference Include="../lib/cs-oead/src/CsOead.csproj" />
</ItemGroup>
```

***Note:** Be sure to replace `lib/cs-oead` with the path you used when adding the [cs-oead](https://github.com/EPD-Libraries/cs-oead) submodule.*

## Loading CsOead

Back in the root project folder, open `Program.cs` and replace the boilerplate code with the following code snippet:

```cs
using CsOead;
using Native.IO.Handles;
using Native.IO.Services;

// Initialize a new INativeLibraryManager in a folder
// named 'native' next to your build files
NativeLibraryManager
    .RegisterPath("native", out bool isCommonLoaded)

    // Register a new OeadLibrary instance
    // (extracts the native libraries)
    .Register(new OeadLibrary(), out bool isOeadLoaded);

// Log the load results
Console.WriteLine(isCommonLoaded);
Console.WriteLine(isOeadLoaded);
```

# Copying & Decompressing the Game Files
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1188s)

Now that [cs-oead](https://github.com/EPD-Libraries/cs-oead) is set up, we need to get the files to edit. For this tutorial, I will simply change the defence value of an armour piece. Similar concepts will apply to other game files though.

Once you've found the file you're going to edit, copy it to another location so you don't mess up your game dump.

## Decompressing

**Important:** If the file you're editing ends with `.zs`, you'll need to decompress it first. You can do this in automatically C#, but for simplicity, I'll just assume you've decompressed it manually beforehand.

This can be done using [TotK ZstdTool](https://github.com/TotkMods/Totk.ZStdTool/releases/latest), which can be downloaded from [github](https://github.com/TotkMods/Totk.ZStdTool/releases/latest).

Download the build for your platform (Windows and Linux supported) and run the exe. Next, open the settings (bottom left) and verify the game path is correctly set (it will pick up the nx-editor config automatically if you set that up earlier).

Finally, drag the `.zs` file over the file path (top field), click `Decompress` and save the output file.

## Plan your Edits

Once your file is prepped you should plan your edits. This can be done using any visual editor for the file format, but I recommend using [NX-Editor](https://nx-editor.github.io/). If you set up [NX-Editor](https://nx-editor.github.io/) earlier, simply open your chosen file by dragging it over the main window.

In my case, I chose a SARC file (.pack) to edit, so it opens a file tree of the archive. I know that in TotK the `BaseDefense` value is stored in the BYML file (.bgyml) `Component/ArmorParam/Armor_001_Head.game__component__ArmorParam.bgyml`. I can then open the file and preview the BYML file converted to YAML.

Inside the YAML I can see the `BaseDefense` key, which is the value I want to edit in my script.

Now we have all the information we need:

- The game file to edit (decompressed): `/Pack/Actor/Armor_001_Head.pack`
- The file inside the SARC to edit: `Component/ArmorParam/Armor_001_Head.game__component__ArmorParam.bgyml`
- And finally, the key inside the BYML to edit: `BaseDefense`

# Loading the File in Memory
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1360s)

To edit the game file we first need to load it into our application memory as raw data.

This is quite trivial to do using the `File` class provided in the dotnet clr in `System.IO`

If you're ***not*** using *implicit usings*, add the `System.IO` using statement to the top of the file.

```cs
using System.IO;
```

First, we need to open a `FileStream` into our copied game file from earlier.

```cs
using FileStream fs = File.OpenRead(".\\Armor_001_Head.pack");
```

Once that's in place we need to create a new `byte[]` big enough to store the raw file data. This can be done as follows.

```cs
byte[] buffer = new byte[fs.Length];
```

Finally, we need to read the file into our `byte[]`, this can be done using the `FileStream.Read` method like this.

```cs
fs.Read(buffer);
```

Now you should have a code snippet similar to the following:

```cs
using FileStream fs = File.OpenRead(".\\Armor_001_Head.pack");
byte[] buffer = new byte[fs.Length];
fs.Read(buffer);
```

## Alternative Method

This could also be written in one line like this:

```cs
byte[] buffer = File.ReadAllBytes(".\\Armor_001_Head.pack");
```

The only reason I did it the other way is because it explains what is happening better.

# Parsing a SARC
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1430s)

Now that we have the file buffer, we need to parse it into a SARC object to extract the sub-file data.

To load a SARC object from a `byte[]` we need to use the `Sarc.FromBinary` function.

```cs
Sarc sarc = Sarc.FromBinary(buffer);
```

Now that we have the `Sarc` in memory, we need to get the sub-file data from it. This is done using the indexer, which takes in a `string key` and returns a `ReadOnlySpan<byte>`.

```cs
ReadOnlySpan<byte> data = sarc["Component/ArmorParam/Armor_001_Head.game__component__ArmorParam.bgyml"];
```

# Parsing a BYML
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1523s)

Becuse we planned our edits earlier, we know that the data return from the SARC is a BYML (.bgyml) file. So we can use the `Byml` class to parse the raw data into an editable object.

```cs
Byml byml = Byml.FromBinary(data);
```

Next, we just need to change the `BaseDefense` value located earlier. To do this we will first need to get the container holding all the keys/values in order to query for `BaseDefense` and set the value. In this case, the root container is a hash (similar to a [Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-7.0)) so we will use `Byml.GetHash`.

```cs
BymlHash hash = byml.GetHash();
```

Finally, we simply query the hash for our key and set the value.

```cs
hash["BaseDefense"] = 6;
```

# Saving the Modified Files
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1670s)

Now that the file is modified in memory (the application), we need to write it back to a file. Doing this requires getting a `byte[]` from our `Sarc` and `Byml` instances. This is called [serializing](https://en.wikipedia.org/wiki/Serialization) and can be done with the `ToBinary` function; which, returns an unsafe `byte[]` wrapped in the `DataMarshal` class.

So with a rough plan in mind, the first thing we need to do is set the SARC file data to our edited BYML data. This is done in the same way we got the data, except instead of collecting the `ReadOnlySpan<byte>` we set it to the serialized BYML file.

```cs
DataMarshal outputByml = byml.ToBinary(Endianness.Little, version: 7);
sarc["Component/ArmorParam/Armor_001_Head.game__component__ArmorParam.bgyml"] = outputByml;
```

Now that the SARC is also edited, we need to serialize it and write that to a file using a `FileStream`.

```cs
DataMarshal outputSarc = sarc.ToBinary(Endianness.Little);
using FileStream output = File.Create(".\\Armor_001_Head.output.pack");
output.Write(outputSarc)
```

**Note:** Using `File.WriteAllBytes(".\\Armor_001_Head.output.pack", outputSarc.ToArray())` is ***NOT*** the same as using a `FileStream`. It will copy the data an additional time, which can waste processing time.

# Checking the Results
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1862s)

If you followed all the previous steps, you should be left with a file similar to this.

```cs
using CsOead;
using Native.IO.Handles;
using Native.IO.Services;

NativeLibraryManager
    .RegisterPath("D:\\Bin\\native", out bool isCommonLoaded)
    .Register(new OeadLibrary(), out bool isOeadSuccess);

Console.WriteLine(isCommonLoaded);
Console.WriteLine(isOeadSuccess);

using FileStream fs = File.OpenRead(".\\Armor_001_Head.pack");
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

using FileStream output = File.Create(".\\Armor_001_Head.output.pack");
output.Write(outputSarc.AsSpan());
```

Once you've reviewed your script, run it using `dotnet run` or using your IDE/editor's debugger. If done correctly it should write the file to your specified output, open that with [NX-Editor](https://nx-editor.github.io/) and make sure your changes are applied.

# Summary
> [Timestamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=1877s)

If you need any help or have any questions, feel free to ask me on my [Discord server](https://discord.gg/8Saj6tTkNB).
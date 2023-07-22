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

# Tools & Setup
> [Timetamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=13s)

## IDE or Text Editor

First step is too download and install all the required software to do this.

Obviously you'll need an IDE or text editor, I would highly recomend using **Visual Studio** if you're on Windows or MacOS, and **Visual Studio Code** *(yes they're different)* on Linux.

- [Visual Studio](https://visualstudio.microsoft.com/)
- [Visual Studio Code](https://code.visualstudio.com/)

Both of these are installed with a relatively straight forward installer.

## Other Software

The next couple things you'll need are the [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), the [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022), [MinGW](https://github.com/brechtsanders/winlibs_mingw/releases/tag/13.1.0-16.0.5-11.0.0-ucrt-r5), [Ninja](https://ninja-build.org/), [CMake](https://cmake.org/), and (optionally) [NX-Editor](https://nx-editor.github.io/) for viewing the game files before scripting edits.

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022)
- [MinGW](https://github.com/brechtsanders/winlibs_mingw/releases/tag/13.1.0-16.0.5-11.0.0-ucrt-r5)
- [Ninja](https://ninja-build.org/)
- [CMake](https://cmake.org/)
- [NX-Editor](https://nx-editor.github.io/)

For the [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), the [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#visual-studio-2015-2017-2019-and-2022) and [CMake](https://cmake.org/), just run the downloaded setup exe and follow the steps to install.

As for [MinGW](https://github.com/brechtsanders/winlibs_mingw/releases/tag/13.1.0-16.0.5-11.0.0-ucrt-r5) and [Ninja](https://ninja-build.org/), these are both installed by extractign the zip archives and copying the contents to a folder of your choice (you'll need to [add these to PATH]() later, so keep in mind where you put it).

## Adding MinGW and Ninja to the PATH
> [Timetamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=350s)

In order for the commands used in this tutorial to work, you'll need to add MinGW and Ninja to the system PATH.

*(Likely not required for Linux if you used a app manager)*

### Windows

In the Windows search bar, look for the control panel application called `Edit the system environment variables` and open the application. Once that window loads, click the button to the bottom right labelled `Environment Variables...`

Under the **System variables**, scroll down until you find a key labelled `Path`, double click the entry to edit it.

Finally, click the button on the top right labelled `New` and enter the path to the Ninja folder *(folder containing `ninja.exe`)* in the new entry. Do the same for the `MinGW` path, **and make sure to append `\bin` to the end of the path**.

# NX-Editor Setup
> [Timetamp](https://www.youtube.com/watch?v=pKMcCp1WjoU&t=305s)

To setup NX-Editor, simply run the downloaded `exe` and provide your game path in the settings page.

The field will turn green if the game path is valid. If it does not, make sure your dump is valid and you've directed it to a folder containing the game folders *(not a folder containing a `romfs` or title id folder)*.

<img src="assets/game_dump.png" width=500>
## Introduction

Paste the contents of the clipboard into files.

* Custom text extension rules.
* Automatically identify the image file extension, and support the transparent PNG and animated GIF.
* Faster and more convenient [Monitor Mode](https://github.com/huiyadanli/PasteEx/wiki#%E7%9B%91%E5%90%AC%E6%A8%A1%E5%BC%8F).

Support File (.*), HTML (.html), Image (.png .jpg .gif .bmp .ico ), RTF (.rtf ), Text (.txt .*), etc.

## Usage

![Screenshot](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot.png)

### Requirements

* Windows 7 or higher
* [.NET Framework 4.5](https://www.microsoft.com/zh-cn/download/details.aspx?id=30653) or higher

### Install

Download the latest PasteEx from [here](https://github.com/huiyadanli/PasteEx/releases).

Unzip and run `PasteEx.exe`, and add the **right-click menu** according to the prompts (the UAC prompt will be displayed when the Context Menu is added, please allow it).

### Uninstall

This is a portable software. Just remove the right-click menu on the "Settings", and then delete the folder where the software is located.

If you delete PasteEx but forget to remove the right-click menu, or cannot remove the related function items of the right-click menu normally, you can use the following method.

Copy the following text and save it as a file with a `.bat` extension. Then, right-click on it to open a contextual menu. Click on the "Run as administrator" option to run it.

```
@echo off
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\Background\shell\PasteEx" /F
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\PasteEx" /F
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\Background\shell\PasteExFast" /F
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\PasteExFast" /F
echo Cleaned up
pause
```

## Advanced usage

### Command Line

Users can use the command line to call some functions of PasteEx.

```
PasteEx.exe <action> [-<param>] [<Directory or file path>] 

命令:

reg        Add right-click menu
           -n Add right-click menu to paste in normal mode (Either of the -n and -q arguments must be selected)
           -q Add right-click menu to paste in quick mode (Either of the -n and -q arguments must be selected)
           -s press Shift key and right-click to display (optional)

unreg      Remove right-click menu
           -n Remove right-click menu to paste in normal mode (Either of the -n and -q arguments must be selected)
           -q Remove right-click menu to paste in quick mode (Either of the -n and -q arguments must be selected)

monitor    start Monitor Mode

paste      Paste as file（this action string can be omitted）, The default mode is normal mode, pen the main interface for operation. It must be followed by a directory parameter
           -q Use quick mode to paste into a file.It must be followed by a directory or file path parameter
           -f When pasting as file in quick mode, if the file  already exists, overwrite directly without pop-up prompt (it needs to be used with the -q parameter).

Example:

PasteEx.exe reg -n
Add right-click menu.

PasteEx.exe reg -n -s
Add right-click menu, press Shift key and right-click to display.

PasteEx.exe reg -q
Add Quick paste Mode PasteEx right-click menu.

PasteEx.exe reg -q -s
Add Quick paste Mode right-click menu, press Shift key and right-click to display.

PasteEx.exe unreg -n
Remove right-click menu.

PasteEx.exe unreg -q
Remove Quick paste Mode right-click menu.

PasteEx.exe monitor
start Monitor Mode.

PasteEx.exe "c:\"
Execute paste as file in "c:\" , open the main interface for operation.

PasteEx.exe -q "c:\"
Execute paste as file in "c:\" , Generating files directly, the file name is automatically generated.

PasteEx.exe -q "c:\test.png"
Save the contents of the clipboard as "c:\test.png" directly. When the file already exists, you will be asked whether to cover it.

PasteEx.exe -q -f "c:\test.png"
Save the contents of the clipboard as "c:\test.png" directly. The file will be overwritten directly when it already exists.

```

### Custom text extension rules

Rule format: extension = the first line signature of the text corresponding to this extension (support regular expression)

For text files, the first non-blank line will be used to match the signatures.

If the matching is successful, the corresponding custom extension will be used by default when saving.

Example:

```
cs=^using .*;$
java=^package.*;$
html=(?i)<!DOCTYPE html
cpp=^#include.*
```

### Custom file name generation

Note: The custom file name cannot contain illegal characters `\ / : * ? " < > |`。

+ P.S.: Except for `\`, which can be used to create directory, but may not appear inside `$$`.

To use the date format string of C# DateTime, please use `$` to wrap the string to be formatted.

For example, `\$MM$\$dd$\Clip_%HHmmss$` will copy different clipboard screenshots into different directories based on the date.

Date and time format strings：

| Format specifier | Description                                    |
| ---------------- | ---------------------------------------------- |
| yyyy             | The year as a four-digit number.               |
| yy               | The year, from 00 to 99.                       |
| MM               | The month, from 01 through 12.                 |
| M                | The month, from 1 through 12.                  |
| dddd             | The full name of the day of the week.          |
| ddd              | The abbreviated name of the day of the week.   |
| dd               | The day of the month, from 01 through 31.      |
| d                | The day of the month, from 1 through 31.       |
| HH               | The hour, using a 24-hour clock from 00 to 23. |
| H                | The hour, using a 24-hour clock from 0 to 23.  |
| hh               | The hour, using a 12-hour clock from 01 to 12. |
| h                | The hour, using a 12-hour clock from 1 to 12.  |
| mm               | The minute, from 00 through 59.                |
| m                | The minute, from 0 through 59.                 |
| ss               | The second, from 00 through 59.                |
| s                | The second, from 0 through 59.                 |

Official document：

[Standard date and time format strings](https://docs.microsoft.com/dotnet/standard/base-types/standard-date-and-time-format-strings)

[Custom date and time format strings](https://docs.microsoft.com/dotnet/standard/base-types/custom-date-and-time-format-strings)

### Quick mode

Add the right-click menu of "Quick Paste As File" in the "Settings".
After use, the main interface will not be displayed, and the file will be directly pasted.

The first extension that is analyzed is used for pasting.

### Monitor mode

![start monitor mode](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_m1.png)

Click the "Settings" icon, select the "Monitor Mode" from the drop-down menu.
PasteEx will turn on the monitor mode and show in the icon in the notification area.

If you want to turn on the "Monitor Mode" directly when you start PasteEx,
you can check "Default startup monitor mode" on the "Settings".

![monitor mode started](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_m2.png)

The functions that can be used at this time are as follows:

* In any directory, use the shortcut key `Ctrl + Alt + X` (default) to quickly paste.
* When the "Auto Image to File" option is enabled, the copied image is automatically converted to a file type and appended to the clipboard, which means that `Ctrl + V` can be directly pasted as a file.
  * After Windows 10 1803, Microsoft introduces a hotkey `Win + Shift + S` to take a screenshot from a selected area and copy it to the clipboard. With "Auto Image to File" option enabled, you can save these screenshots conveniently.
* When both the "Auto Image to File" option and the "Auto Save" option in "Settings" are turned on, PasteEx can automatically collect all copied pictures into the specified folder.

#### Application filter

In the monitor mode, the copy source can be filtered, and the clipboard data from the application is not processed,
which can solve some compatibility problems of the same type of software. For detailed settings, see [Application filter](https://github.com/huiyadanli/PasteEx/wiki/App-Filter).

## Contribution

### TODO

It may be plug-in in the future.

[https://github.com/huiyadanli/PasteEx/projects](https://github.com/huiyadanli/PasteEx/projects)

### How to translate

VS 2017 and .Net 4.5 or newer required.

Reference: [C# i18n](https://github.com/Luke31/i18n-cs/wiki/C-Sharp)

## Copyright

PasteEx is licensed under [GPLv3](http://www.gnu.org/licenses/quick-guide-gplv3.html).

Some codes refer to [PasteIntoFiles](https://github.com/EslaMx7/PasteIntoFiles).

Config storage uses  [PortableSettingsProvider](https://github.com/crdx/PortableSettingsProvider).

The software icon uses the "interaction-Assets-icons" icon group (Creative Commons Attribution-Share Alike 3.0 Unported License).

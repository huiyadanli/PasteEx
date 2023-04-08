## 介绍

把剪贴板的内容粘贴为文件。并具有以下特性：

* 自定义文本扩展名规则。
* 自动识别图片的扩展名，且支持透明 PNG 与动态 GIF 的粘贴。
* 更快捷方便的[监听模式](https://github.com/huiyadanli/PasteEx/wiki#%E7%9B%91%E5%90%AC%E6%A8%A1%E5%BC%8F)

支持文件 ( .\* ) 、HTML ( .html ) 、图片 ( .png .jpg .gif .bmp .ico ) 、RTF ( .rtf ) 、文本 ( .txt .\* ) 等内容的分析与粘贴为文件。

## 使用说明

![Screenshot](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot.png)

### 环境

* Windows 7 或更高版本。
* [.NET Framework 4.5](https://www.microsoft.com/zh-cn/download/details.aspx?id=30653) 或更高版本。

### 运行

从[这里](https://github.com/huiyadanli/PasteEx/releases)下载最新的 PasteEx 。

解压并运行 `PasteEx.exe` ，根据提示**添加右键菜单**即可（添加菜单时会有 UAC 提示，放行即可）。

### 卸载

纯绿色便携软件，只要在设置页面移除右键菜单，然后删除软件所在文件夹即可。

如果你删除了 PasteEx ，却忘记移除右键菜单，或者不能正常移除右键菜单相关功能项，可以使用下面的方法。

复制下列文本并保存为 `.bat` 扩展名的文件，然后右键以管理员权限运行即可。

```
@echo off
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\Background\shell\PasteEx" /F
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\PasteEx" /F
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\Background\shell\PasteExFast" /F
REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\PasteExFast" /F
echo 清理完毕
pause
```

## 高级用法

### 命令行

用户可以使用命令行来调用 PasteEx 的部分功能。

```
PasteEx.exe <命令> [-<参数>] [<目录或文件路径>] 

命令:

reg        添加右键菜单
           -n 添加使用普通模式进行粘贴的右键菜单(-n 与 -q 参数必选其一)
           -q 添加使用快速模式进行粘贴的右键菜单(-n 与 -q 参数必选其一)
           -s 添加的菜单需要按住 shift 才能显示(可选)

unreg      移除右键菜单
           -n 移除使用普通模式进行粘贴的右键菜单(-n 与 -q 参数必选其一)
           -q 移除使用快速模式进行粘贴的右键菜单(-n 与 -q 参数必选其一)

monitor    启动监听模式

paste      粘贴为文件（该命令可以省略），默认为普通模式弹出软件界面，后面必须要有目录参数
           -q 使用快速模式粘贴为文件，后面必须要跟有目录或文件路径参数
           -f 使用快速模式粘贴为文件时，如果文件名重复，不弹出提示直接覆盖(需要配合 -q 参数使用)

示例：

PasteEx.exe reg -n
添加右键菜单

PasteEx.exe reg -n -s
添加右键菜单, 按住 shift 时才能显示

PasteEx.exe reg -q
添加快速粘贴右键菜单

PasteEx.exe reg -q -s
添加快速粘贴右键菜单, 按住 shift 才能显示

PasteEx.exe unreg -n
移除右键菜单

PasteEx.exe unreg -q
移除快速粘贴右键菜单

PasteEx.exe monitor
启动监听模式

PasteEx.exe "c:\"
在 c:\ 执行粘贴为文件, 弹出主界面进行操作

PasteEx.exe -q "c:\"
在 c:\ 执行快速粘贴为文件, 直接生成文件, 文件名由程序自动生成

PasteEx.exe -q "c:\test.png"
把剪贴板内容直接保存为 c:\test.png，文件已存在时，会询问是否覆盖

PasteEx.exe -q -f "c:\test.png"
把剪贴板内容直接保存为 c:\test.png，文件已经存在时会直接覆盖

```

### 自定义文本扩展名规则

规则格式：扩展名=与此扩展名相对应文本的第一行特征（支持正则）

对于文本类型的文件，将会取第一个非空行对特征进行匹配，
匹配成功则在保存时默认使用对应的自定义扩展名。

比如：

```
cs=^using .*;$
java=^package.*;$
html=(?i)<!DOCTYPE html
cpp=^#include.*
```

### 自定义文件名生成语法

注意自定义文件名中不能含有非法字符 `\ / : * ? " < > |`。

+ P.S.: `\`除外，其可用于创建分类目录，但不可以在 `$$`内出现。

使用 C# DateTime 的日期格式化语法，请使用 `$` 包裹需要格式化的字符串。

比如：`$yyyy$\$MM$\$dd$\Clip_%HHmmss$` 即可实现根据日期将不同的剪贴板截图复制到不同的目录内。

日期格式化语法：

| 符号 | 说明         |
| ---- | ------------ |
| yyyy | 年 (四位数)  |
| yy   | 年 (两位数)  |
| MM   | 月份 (01-12) |
| M    | 月份 (1-12)  |
| dddd | 星期几       |
| ddd  | 周几         |
| dd   | 日期 (01-31) |
| d    | 日期 (1-31)  |
| HH   | 时 (00-23)   |
| H    | 时 (0-23)    |
| hh   | 时 (01-12)   |
| h    | 时 (1-12)    |
| mm   | 分 (00-59)   |
| m    | 分 (0-59)    |
| ss   | 秒 (00-59)   |
| s    | 秒 (0-59)    |

更多语法及详情参见官方文档：

[标准日期和时间格式字符串](https://docs.microsoft.com/dotnet/standard/base-types/standard-date-and-time-format-strings)

[自定义日期和时间格式字符串](https://docs.microsoft.com/dotnet/standard/base-types/custom-date-and-time-format-strings)

### 快速模式

在设置页面添加 “快速粘贴为文件” 右键菜单，使用后不显示主界面，直接粘贴为文件。

默认使用分析出的首位扩展名进行粘贴。

### 监听模式

![开启监听模式](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_m1.png)

点击设置图标，在下拉菜单中选择监听模式，开启监听模式。PasteEx 将常驻在右下角托盘图标中。

如果你想要在双击打开 PasteEx 的时候直接开启监听模式，可以在设置页面勾选 "默认开启监听模式"。

![监听模式启动](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_m2.png)

此时可以使用的功能如下：

* 在任意目录，使用快捷键 `Ctrl + Alt + X` （默认）进行快速粘贴。
* 自动图片转文件选项打开时，自动把复制的图片转换为文件类型并追加到剪贴板，也就是说 `Ctrl + V` 可以直接粘贴为文件。
  * Tips: Windows 10 1803 及之后的版本支持热键 `Win + Shift + S` 进行快捷截图并把截图复制到剪贴板，配合可以快速高效地保存截图。
* 自动图片转文件选项打开时，设置中的自动保存选项可以把所有复制的图片自动收集到指定文件夹。

#### 应用过滤

监听模式下可以针对复制来源进行过滤，不处理来自该应用的剪贴板数据，可以解决一些同类型软件的兼容问题。详细设置请见[应用过滤](https://github.com/huiyadanli/PasteEx/wiki/App-Filter)

## 参与&贡献

### TODO

后面会逐渐加上插件化的内容，便于添加各类自定义功能。

[https://github.com/huiyadanli/PasteEx/projects](https://github.com/huiyadanli/PasteEx/projects)

### How to translate

VS 2017 and .Net 4.5 or newer required.

Reference: [C# i18n](https://github.com/Luke31/i18n-cs/wiki/C-Sharp)

## 版权许可

PasteEx 使用 [GPLv3](http://www.gnu.org/licenses/quick-guide-gplv3.html) 协议进行发布。

部分代码参考自 [PasteIntoFiles](https://github.com/EslaMx7/PasteIntoFiles)。

用户配置存储，直接使用了 [PortableSettingsProvider](https://github.com/crdx/PortableSettingsProvider) (MIT)。

软件图标使用了 interaction-Assets-icons 图标组 (Creative Commons Attribution-Share Alike 3.0 Unported License)。

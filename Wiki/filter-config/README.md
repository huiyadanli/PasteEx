## 应用过滤

![应用过滤设置](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_s1.png)

监听模式下，可以设置针对复制来源的过滤。对 "排除" 的应用放入剪切板的数据不做处理，或者仅对来自 "包括" 的应用剪切板数据做处理。

主要是为了不和同类软件冲突而添加的设置项，比如与 Ditto 的兼容性问题 [#11](https://github.com/huiyadanli/PasteEx/issues/11)

默认使用 "排除" 进行过滤，即排除设置的复制来源。

多个复制来源请用 `|` 分割，比如 `chrome|firefox|QQ`

### 如何获得复制来源的名字

一般情况下复制来源的名字就是对应的进程名，直接打开任务管理器就能看到。

但是有些复制来源完全不能知道到底是所属于哪个进程，比如系统自带的 PrtSc 键截图、 QQ 截图等，截图后会把图片塞入剪切板。

![打开 Debug 窗口](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_m3.png)

此时就要用到 Debug 窗口，右键 PasteEx 的托盘图标 -> 高级 -> 打开 Debug 窗口，一个控制台窗口就打开了。

![Debug 窗口](https://raw.githubusercontent.com/huiyadanli/PasteEx/master/Screenshot/Screenshot_c1.png)

按下 PrtSc 键可以看到 Clipboard Owner (剪切板所有者，也就是复制来源) 是 Idle ，直接在设置中排除 Idle 就能过滤所有来自 Idle 的剪切板消息了。也就是说，设置排除后，监听模式不会去自动处理来自 PrtSc 键截图放入剪切板的图片。

### 与同类软件的冲突解决

如果有同类带有剪切板监听功能的软件，很有可能会与 PasteEx 产生兼容性问题。

如果该软件有应用过滤的功能，可以把 `PasteEx` 加入它的排除列表中。比如 Ditto ，Ditto 与 PasteEx 有双向兼容性问题，不仅要把 PasteEx 加入 Ditto 的排除列表，同时也要把 Ditto 加入到 PasteEx 的排除列表中。产生问题具体原因 [#11](https://github.com/huiyadanli/PasteEx/issues/11) 中有做解答。

如果该软件没有过滤的功能，那只能只能在使用其中一个的时候关闭另一个的功能。PasteEx 可以停止监听剪切板，或者取消勾选 "自动图片转文件" 这项功能。




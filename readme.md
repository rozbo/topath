
## 它存在的意义
作为一个程序员，我最不能容忍`windows`的地方，其实是它的命令行交互，和各种unix系的相比，简直是屎一样不可忍受。但是有的时候，你还必须得在`windows`下工作，简直是逼着吃屎。那怎么办呢，我们只能硬着头皮吃了呗，还能怎么样。。。
这些屎里面最难吃的是他的`path`机制，随着我们安装的第三方工具越来越多，为了能在命令行下使用，我们可能需要一直添加各种各样的环境变量，然后退出甚至重启重试。添加这么多环境变量有副作用吗？我理解是有的，包括性能、安全和便捷性方面。最重要的是作为一个强迫症患者，我完全无法忍受。所以，我做了这样一个小工具。

## 它的原理
在unix系列下，我们经常建立一个软连接来实现这一目标，那么在windows下有没有这样的软连接呢？答案是肯定的`mklink`，但这个工具和unix相比简直是屎一样，限制较多。所以我们使用另外一种广泛应用的小技巧，即`批处理中转`,这也是目前应用较为广泛的方案，比如`npm`,`pip`等都是这个原理。说白了，其实非常的简单。

我们建一个文件，名叫`java.cmd`,内容如下。
```bat
@echo off
"D:\path\to\java.exe" %*
```
然后将这个文件放在环境变量的目录里，即可。如此，我们用这种方式来模拟软连接实现，即可达到只添加一个环境变量，以后任意多的文件都可以放进来。

## 它的用法
1. 为了管理方便，我们在环境变量里新建一个统一的目录以管理我们所有的第三方工具，比如`D:\Env\bin`
2. 我们把我们的`topath.exe`放到这里面
3. 此时，我们在命令行下即可使用`topath`命令来映射工具了，它的用法如下
```bat
topath [命令] 可执行文件路径
```
4. 为了使用方便，我们也可以直接添加一个右键菜单，来一键映射，方法见下。



## 右键直接映射

新建一个内容如下的`xxx.reg`的文件，然后导入即可。
```reg
Windows Registry Editor Version 5.00

[HKEY_CLASSES_ROOT\*\shell\topath]
@="topath"

[HKEY_CLASSES_ROOT\*\shell\topath\command]
@="\"D:\\Env\\bin\\topath.exe\" \"%1\""

```
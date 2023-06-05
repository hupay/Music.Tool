# 说明
音乐工具类。接口及部分实现参考[musicdl](https://github.com/CharlesPikachu/musicdl)。

# 诞生原因
逛GitHub时，发现这种项目非常适合高级语言特性（封装，继承，多态）的学习，于是就有了这个项目。

# 项目创建命令
锻炼下手打命令的能力。试试用vs code编码，好吧，还是不习惯，用vs吧。

```bash
# 创建项目目录
mkdir Music.Tool
cd Music.Tool

# 新建解决方案。默认当前目录为解决方案名称。
dotnet new sln
# 新建类库
dotnet new classlib -n Music.Tool -f net6.0 -lang c# -o src/Music.Tool

# 将项目添加到解决方案，会带上src目录
dotnet sln add src/Music.Tool/Music.Tool.csproj
dotnet sln remove src/Music.Tool/Music.Tool.csproj

# 将项目添加到解决方案的根目录
dotnet sln add src/Music.Tool/Music.Tool.csproj --in-root

dotnet new list

# 建立gitignore文件
dotnet new gitignore

code . # vs code 打开

# 构建
dotnet build
```
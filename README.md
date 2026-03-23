# UniversalRadialMenu

无限轮盘 - 独立维护分支（基于原版反编译，含 Bug #1-7 修复）。

这个项目不是“单独一个 exe 就能直接给别人用”的类型，而是一个 **Quicker 动作 + 外部 exe** 的联合分发项目：

- **Quicker 动作 JSON** 是入口，负责安装、启动、更新。
- **`UniversalRadialMenu_new.exe`** 是真正运行轮盘的外部程序。

如果只把 `exe` 发给别人，而没有同时提供动作 JSON 和安装说明，最终用户通常是装不起来的。

## 修复内容

| Bug | 描述 | 修复 |
|-----|------|------|
| #1 | 焦点恢复后发送快捷键过早 | 等待时间 40ms → 200ms |
| #2 | 键入文本同理 | 同上 |
| #3 | 其余 4 种操作类型同理 | 同上 |
| #4-#7 | 更多边界修复 | 详见提交记录 |

## 先说结论：用户该怎么安装

### 推荐方式：自动安装

这是当前面向最终用户的**主安装方式**。

1. 先安装 Quicker。
2. 下载并导入对应版本的动作 JSON（例如：`UniversalRadialMenu_action_vX.Y.Z.json`）。
3. 运行动作一次。
4. 动作会自动下载并解压外部程序。
5. 看到“安装成功，请重新运行动作！”后，再运行动作一次。

第二次运行后，轮盘才算真正可用。

### 兜底方式：手动部署文件

手动方式的本质不是“手动跑一个独立安装器”，而是**把外部程序文件放到动作约定的固定目录里**。

它适用于这些情况：

- 自动下载失败；
- 想手动覆盖同版本或新版本文件；
- 需要修复损坏的外部程序文件；
- 需要离线准备 exe 文件。

但要注意：**当前动作的路径绑定是在安装分支里完成的**。因此，**全新机器的首次安装仍建议优先走自动安装**。手动方式更适合作为修复、覆盖更新、自动安装失败后的兜底手段。

## 完整发布物说明

一个对外可用的 Release，至少应该同时提供这 3 个文件：

| 文件 | 作用 | 用户是否需要手动处理 |
|------|------|----------------------|
| `UniversalRadialMenu_action_vX.Y.Z.json` | Quicker 动作入口；负责安装、启动、更新 | **需要导入** |
| `UniversalRadialMenu_vX.Y.Z.7z` | 外部程序压缩包，内含 `UniversalRadialMenu_new.exe` 和 `.config` | 自动安装时不用管；手动部署时需要 |
| `7zip.zip` | 自动安装时使用的内置解压工具 | **不需要**，这是动作内部依赖 |

发布资产建议统一放在仓库的 [Releases](https://github.com/konbakuyomu/UniversalRadialMenu/releases) 页面。

### 重要提醒

- **只有 `UniversalRadialMenu_vX.Y.Z.7z` 不够。**
- **只有 `exe` 不够。**
- 最终用户真正的入口始终应该是 **动作 JSON**。

## 固定安装目录

当前实现不是“用户自由选择安装目录”的模式，而是固定目录模式。

自动安装或手动部署后，外部程序应位于：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\
```

这个目录下应至少存在：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\UniversalRadialMenu_new.exe
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\UniversalRadialMenu_new.exe.config
```

程序配置文件位于：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\RadialMenu_AppConfig.xml
```

说明：

- `RadialMenu_AppConfig.xml` 第一次正常启动后会自动创建。
- 如果这个文件还不存在，但 `exe` 已经放对目录，这是正常现象。

## 自动安装链路（推荐）

### 用户视角

1. 安装 Quicker。
2. 导入动作 JSON。
3. 第一次运行动作。
4. 等待动作自动完成下载与解压。
5. 收到“安装成功，请重新运行动作！”提示。
6. 再运行动作一次。

### 实际发生了什么

第一次运行时，动作会：

1. 检查内部保存的 `exe文件夹路径` 是否为空。
2. 如果为空，就进入安装分支。
3. 下载：
   - `7zip.zip`
   - `UniversalRadialMenu_vX.Y.Z.7z`
4. 将 `7zip.zip` 解压到：

```text
%USERPROFILE%\Documents\Quicker\LDH\7zip\
```

其中关键文件是：

```text
%USERPROFILE%\Documents\Quicker\LDH\7zip\7za.exe
```

5. 将 `UniversalRadialMenu_vX.Y.Z.7z` 先保存为：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘.7z
```

6. 再把这个 `.7z` 解压到：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\
```

7. 动作把“解压后文件夹路径”保存到变量 `exe文件夹路径`。
8. 后续动作统一从下面这个路径启动轮盘：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\UniversalRadialMenu_new.exe
```

这就是自动安装时“动作和外部 exe 是怎么绑定路径”的完整过程。

### 为什么第一次安装后要再运行一次

因为第一次运行的主要任务是：

- 下载文件；
- 解压文件；
- 保存 `exe文件夹路径`；
- 让动作和外部 exe 建立固定路径绑定。

所以第一次更像“部署”，第二次才是“正式使用”。

## 手动部署链路（兜底）

### 你需要准备什么

手动部署时，用户真正需要关心的只有两个东西：

- 动作 JSON
- `UniversalRadialMenu_vX.Y.Z.7z`

`7zip.zip` **不需要用户自己处理**。它只是动作自动安装时内部用的解压组件。

### 正确步骤

1. 先导入动作 JSON。
2. 下载 `UniversalRadialMenu_vX.Y.Z.7z`。
3. 用任意能解压 `.7z` 的工具手动解压。
4. **目标目录必须是：**

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\
```

5. 解压完成后，确认目录下至少有：

```text
UniversalRadialMenu_new.exe
UniversalRadialMenu_new.exe.config
```

6. 再运行动作。

### 最容易出错的地方

**不要把文件解压到 `LDH\` 根目录。**

错误示例：

```text
%USERPROFILE%\Documents\Quicker\LDH\UniversalRadialMenu_new.exe
```

正确示例：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\UniversalRadialMenu_new.exe
```

也就是说，**`无限轮盘` 这一层子目录不能少**。

### 手动部署与路径绑定的关系

当前动作内部并不是“每次都全盘扫描硬盘找 exe”，而是依赖保存下来的 `exe文件夹路径` 变量。

这意味着：

- **自动安装**会自动写入这个路径变量。
- **手动部署**只是把文件放到正确位置。

因此，在当前实现下：

- 如果你只是修复、覆盖、替换已经安装好的文件，手动部署后直接再运行动作即可。
- 如果你是在**全新机器**上做第一次安装，仍然建议至少成功走一次自动安装分支，让动作把 `exe文件夹路径` 正式记录下来。

换句话说，当前版本的“手动安装”更准确地说是：

- **手动部署外部程序文件**
- **不是完全脱离动作安装分支的独立安装器**

## 更新方式

### 方式 1：动作层更新

如果更新只涉及动作逻辑或动作本身：

1. 导入新版动作 JSON，替换旧动作。
2. 再次运行动作。

### 方式 2：外部 exe 更新

如果更新包含 `UniversalRadialMenu_new.exe`：

- 推荐继续走动作自动更新；
- 如果自动更新失败，可以手动把新版 `UniversalRadialMenu_vX.Y.Z.7z` 解压覆盖到：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\
```

覆盖前建议先退出正在运行的轮盘进程。

## 运行前提

### Quicker 安装位置

当前程序默认按 Quicker 标准安装位置工作，预期位置为：

```text
C:\Program Files\Quicker\
```

程序还会尝试从以下标准目录解析 Quicker 相关 DLL：

```text
C:\Program Files\Quicker\
C:\Program Files (x86)\Quicker\
```

因此：

- 建议使用 Quicker 的标准安装方式；
- 不建议把 Quicker 改成未知的便携路径后再来反馈兼容问题。

### 运行时环境

- .NET Framework 4.8
- Windows

## 排障

### 1. 我到底该下载哪个文件？

普通用户优先下载：

- `UniversalRadialMenu_action_vX.Y.Z.json`

如果自动安装失败，再额外下载：

- `UniversalRadialMenu_vX.Y.Z.7z`

`7zip.zip` 一般不用用户自己碰。

### 2. 为什么我导入动作后第一次运行没弹出轮盘？

因为第一次通常是在执行安装/部署，不是正式运行。  
看到“安装成功，请重新运行动作！”后，再运行一次。

### 3. 为什么我手动解压了还是不工作？

优先检查这几件事：

1. 是否解压到了正确目录：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\
```

2. 是否误解压到了 `LDH\` 根目录。
3. 是否缺少：
   - `UniversalRadialMenu_new.exe`
   - `UniversalRadialMenu_new.exe.config`
4. 是否导入了对应版本的动作 JSON。

### 4. 日志在哪

程序目录日志：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\UniversalRadialMenu_Debug.log
```

崩溃日志：

```text
%USERPROFILE%\Documents\UniversalRadialMenu_Crash.log
```

### 5. 配置文件在哪

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\RadialMenu_AppConfig.xml
```

### 6. 我能不能手动指定安装路径？

当前版本**不支持**面向最终用户的自定义安装目录。

原因不是文档没写，而是实现本身就是固定目录模型：

- 动作安装逻辑按固定目录下载和解压；
- 程序配置路径按固定目录保存；
- 动作后续启动也依赖固定路径绑定。

如果以后要支持自定义路径，需要同时修改：

- 动作 JSON 的路径状态逻辑；
- exe 内部配置路径逻辑；
- 更新逻辑；
- 配置迁移逻辑。

## 构建

- 目标框架：.NET Framework 4.8
- 用 Visual Studio 2022 或 `dotnet build` 编译

## 面向维护者的 Release 建议

对外发布时，建议至少同时上传：

- `UniversalRadialMenu_action_vX.Y.Z.json`
- `UniversalRadialMenu_vX.Y.Z.7z`
- `7zip.zip`

并在 Release 说明中明确写出：

1. 推荐用户优先导入动作 JSON；
2. 首次运行会自动下载并部署外部 exe；
3. 若自动安装失败，手动把 `.7z` 解压到：

```text
%USERPROFILE%\Documents\Quicker\LDH\无限轮盘\
```

4. `7zip.zip` 只是动作内部依赖，不是用户手动安装包。

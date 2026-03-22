# UniversalRadialMenu

无限轮盘 - 独立分支（基于原版反编译，含 Bug #1-7 修复）

## 修复内容

| Bug | 描述 | 修复 |
|-----|------|------|
| #1 | 焦点恢复后发送快捷键过早 | 等待时间 40ms → 200ms |
| #2 | 键入文本同理 | 同上 |
| #3 | 其余 4 种操作类型同理 | 同上 |
| #4-#7 | 更多边界修复 | 详见提交记录 |

## 构建

- 目标框架：.NET Framework 4.8
- 用 Visual Studio 2022 或 `dotnet build` 编译

## 使用

编译后的 `UniversalRadialMenu.exe` 需重命名为 `UniversalRadialMenu_new.exe`，放入 Quicker 动作的运行时目录。

## Release

预编译的 exe 可在 [Releases](https://github.com/konbakuyomu/UniversalRadialMenu/releases) 页面下载。

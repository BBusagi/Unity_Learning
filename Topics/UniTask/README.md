# UniTask

Unity 异步编程学习笔记与练习脚本。

## 内容

- [学习笔记](Note.md)：从原 UniRx 笔记中归入本专题的 UniTask 记录。
- [Scripts](Scripts/)：11 个练习脚本，包括 Chapter01、Chapter02 系列，以及 Runner、UniTaskExample、UniTaskTools。
- [教学场景](Samples/Scenes/)：保留场景加载、UI 与异步操作的示例。
- [示例资源](Samples/Resources/Test.txt)与[材质](Samples/Black.mat)：保留脚本和场景使用的小型资源。

## 使用说明

这些文件按学习专题保存。需要运行时，将 `Scripts`、`Samples/Scenes`、`Samples/Resources` 和示例材质连同对应 `.meta` 导入自己的 Unity 工程。保持 `Assets/Scenes`、`Assets/Resources` 路径，并按场景加载代码配置 Build Settings。

原练习使用 Unity `2021.3.36f1`、TextMesh Pro `3.0.6`，UniTask 依赖来自 `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask`，原锁定提交为 `f9fd769be7c634610f2a61aa914a1a55f34740e1`。脚本还引用了 Unity UI、TextMesh Pro 和 Visual Scripting；运行前需要配置对应依赖。第三方资源及 Unity 工程配置未随专题保存。

## 学习资源

Unity 异步编程学习资源
+ [精选Unity开源库分享及解说](https://space.bilibili.com/69585/channel/collectiondetail?sid=548041) - [gitee](https://gitee.com/yicunjun/share-github-sample/tree/master/Assets/UniTaskTutorial)
+ [官方中文文档](https://github.com/Cysharp/UniTask/blob/master/README_CN.md)
+ [UniTask – Unityでasync/awaitを最高のパフォーマンスで実現するライブラリ](https://tech.cygames.co.jp/archives/3256/)
+ [Deep Dive async/await in Unity with UniTask(UniRx.Async)](https://www.youtube.com/watch?v=irbA4kmq4Wc)
+ [第4回 なぜUniTaskがいいのか？async/awaitとは【Unity使いこなし術】](https://www.youtube.com/watch?v=wCSmZSKG3ig)
+ [UniTaskの使い方2020（スライド＆動画）- Unity Learning Materials](https://learning.unity3d.jp/4615/)

[返回专题目录](../README.md) · [返回仓库目录](../../README.md)

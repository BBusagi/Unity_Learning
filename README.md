# Unity_Learning

个人 Unity 学习仓库，按专题保存插件笔记与脚本，并记录 Unity 项目实践。

## 目录

| 模块 | 内容 | 入口 |
| --- | --- | --- |
| Topics | UniRx、UniTask 学习笔记、脚本、教学场景与资源 | [专题目录](Topics/README.md) |
| Project_SpaceShooter | 射击项目实践、XR 交互与开发记录 | [项目目录](Project_SpaceShooter/) · [学习记录](#project_spaceshooter) |
| Project_VN learning | 视觉小说练习、对话 UI、动画与文本处理 | [学习记录](Project_VN%20learning/README.md) · [项目目录](Project_VN%20learning/) |

## 学习专题

- [UniRx](Topics/UniRx/README.md)：响应式事件、资源释放与操作符练习。
- [UniTask](Topics/UniTask/README.md)：异步操作、PlayerLoop、资源与场景加载练习。

专题主要用于阅读笔记和复用脚本，运行时按各专题 README 配置自己的 Unity 工程。项目目录则保留各自的实践内容与学习记录。

## 相关仓库

[Learning_CS](https://github.com/BBusagi/Learning_CS) 保存通用 C#、LINQ、.NET、其他语言、算法与设计模式笔记。UniRx 和 UniTask 的学习内容已从该仓库迁入本仓库。

## TODO
[【unity ebook】9 ways to optimize your game development](https://create.unity.com/nine-ways-to-optimize-game-development?utm_source=thirdparty&utm_medium=affiliate&utm_campaign=gaming_global_acquisition_2020-01-gmg-ebook2&utm_content=brackeys)
[【Udemy】Lighting in Unity](https://www.udemy.com/course/lighting-in-unity-l/)

## Project_SpaceShooter
[GoogleDrive](https://drive.google.com/drive/folders/1pmB-Sg2tDb274kEB_Wd_TdBK56f7biw6?usp=sharing)

### RoadMap
+ 第一阶段 基本教程
+ 第二阶段 交互模块更改
    + 移动模块（快速移动 远程传送 视角重置 转身设置 跳跃）
    + 菜单 设置按键 装备模式
    + 射击模块（）瞄准线 自动锁定 交互射击
    + 感知增强模块
    + 全身显示模组
    + 近战攻击模组
    + 投掷物模块
    + 击碎效果
    + 敌方ai模块

### DevLog
+ [Bugfix] XR hand no input action
+ [Bugfix] Rendering pipeline converter

### Note
#### Setup
+ XR Rig, XR interaction - default XRI
+ XRI - version 2.6.4

#### XRI
+ remove 'XRRayInteractor' / 'LineRenderer' / 'XR interactor Line Visual'
+ add 'XRDirectInteractor'
+ add 'XR grab interactable'

#### C#
+ MonoSingleton<T>
+ C# interface

#### GamePlay
+ livecycle - OnDrawGizmos
+ World.Space
+ PlayerPrefs

#### UI
+ Anchor Presets - ctrl alt

#### Lighting
+ Light Modes
    + Realtime
    + Baked
    + Mixed
+ Lighting Mode - Mixed
    + Baked Indirect - 全部光线烘焙，动态物体只受间接光，不投影
    + Subtractive - 单光源静态烘焙 + 动态实时（更简化，低端设备推荐）
        > 第一个启用的 Directional Light 为 Main Light。
    + Shadowmask - 静态阴影烘焙，动态阴影实时（更真实）
+ LightMap resolution/Texel Validity (60)
+ Reflection Probe
+ Light probe - 存储间接（反射）光照信息，通常布置在光照环境变化较大（色彩，明暗）区域


#### Animation
+ Animator.SetTrigger

#### PostProcessing
+ PostProcessing
+ Add override - bloom, color adjustment

#### Particle System
+ Emisson, Shape, Renderer
+ Shader

#### Camera
+ Clipping planes

#### Performance Optimization
+ Texture optimisation
+ Build - max texture size: max 1024
+ Maps - reduces texture size（256） if not much complex information
    + Base Map
    + Metallic Map
    + Normal Map
    + Occlusion Map
+ Enable GPU instancing - render multiple identical objects in a single draw call
+ Occulusion Culling - bake

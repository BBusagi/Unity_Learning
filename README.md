# Unity_Learning

XR Interaction

## Project_SpaceShooter
[GoogleDrive]https://drive.google.com/drive/folders/1pmB-Sg2tDb274kEB_Wd_TdBK56f7biw6?usp=sharing

## DevLog
+ [Bugfix] XR hand no input action
+ [Bugfix] Rendering pipeline converter

### Setup
+ XR Rig, XR interaction - default XRI
+ XRI - version 2.6.4

### XRI
+ remove 'XRRayInteractor' / 'LineRenderer' / 'XR interactor Line Visual'
+ add 'XRDirectInteractor'
+ add 'XR grab interactable'

### C#
+ MonoSingleton<T>
+ C# interface

### GamePlay/ C#
+ livecycle - OnDrawGizmos
+ World.Space
+ PlayerPrefs 

### UI
+ Anchor Presets - ctrl alt

### [TODO] Lighting
[【Udemy】Lighting in Unity](https://www.udemy.com/course/lighting-in-unity-l/)  
[【unity ebook】9 ways to optimize your game development](https://create.unity.com/nine-ways-to-optimize-game-development?utm_source=thirdparty&utm_medium=affiliate&utm_campaign=gaming_global_acquisition_2020-01-gmg-ebook2&utm_content=brackeys)
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


### Animation
+ Animator.SetTrigger

### PostProcessing
+ PostProcessing
+ Add override - bloom, color adjustment

### Particle
+ Emisson, Shape, Renderer
+ Shader

### Camera
+ Clipping planes

### Performance Optimization
+ Texture optimisation 
+ Build - max texture size: max 1024
+ Maps - reduces texture size（256） if not much complex information
    + Base Map 
    + Metallic Map
    + Normal Map
    + Occlusion Map
+ Enable GPU instancing - render multiple identical objects in a single draw call
+ Occulusion Culling - bake
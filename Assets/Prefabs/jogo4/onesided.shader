Shader "Sprites/Default"
{
    Properties{
        [PerRenderedData] _MainText ("Sprite Texture", 2D) ="white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRenderedData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags{
                "Queue"="Transparent"
                "IgnoreProjector"="True"
                "RenderType"="Transparent"
                "PreviewType"="Plane"
                "CanUseSpriteAtlas"="True"
        }

        Cull Back
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass{
                CGPROGRAM
                    #pragma vertex SpriteVert
                    #pragma fragment SpriteFrag
                    #pragma target 2.0
                    #pragma multi_compile_instancing
                    #pragma multi_compile_local _ PIXELSNAP_ON
                    #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
                    #include "UnitySprites.cginc"
                ENDCG
        }
    }

}
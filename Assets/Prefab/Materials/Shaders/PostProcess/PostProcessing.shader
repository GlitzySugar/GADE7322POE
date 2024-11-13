Shader "Custom/NeonPostProcessing"
{
    Properties
    {
        _MainTex ("Screen Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (0, 1, 1, 0.3) // Cyan with alpha for blending
        _Saturation ("Saturation", Range(0, 2)) = 1.5     // Saturation boost for vibrancy
        _Brightness ("Brightness", Range(0, 2)) = 1.2     // Slight brightness increase
        _GlowIntensity ("Glow Intensity", Range(0, 1)) = 0.3 // Glow effect strength
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _TintColor;
            float _Saturation;
            float _Brightness;
            float _GlowIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the screen texture color
                fixed4 col = tex2D(_MainTex, i.uv);

                // Apply brightness adjustment
                col.rgb *= _Brightness;

                // Apply saturation adjustment
                float luminance = dot(col.rgb, fixed3(0.3, 0.59, 0.11)); // Calculate perceived brightness
                col.rgb = lerp(fixed3(luminance, luminance, luminance), col.rgb, _Saturation);

                // Add the neon cyan tint with a glow effect
                col = lerp(col, _TintColor, _TintColor.a * _GlowIntensity);

                return col;
            }
            ENDCG
        }
    }
}
Shader "Custom/AutoRotateTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RotationSpeed ("Rotation Speed (°/sec)", Float) = 90
        _Alpha ("Alpha", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _RotationSpeed;
            float _Alpha;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                return o;
            }

            float2 RotateUV(float2 uv, float angleDeg)
            {
                float2 center = float2(0.5, 0.5);
                uv -= center;

                float rad = radians(angleDeg);
                float cosA = cos(rad);
                float sinA = sin(rad);

                float2x2 rotMatrix = float2x2(cosA, -sinA, sinA, cosA);
                uv = mul(rotMatrix, uv);

                uv += center;
                return uv;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float angle = _Time.y * _RotationSpeed;
                float2 rotatedUV = RotateUV(i.uv, angle);
                fixed4 col = tex2D(_MainTex, rotatedUV);
                col.a *= _Alpha;
                return col;
            }
            ENDCG
        }
    }
}

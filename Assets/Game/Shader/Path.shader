Shader "Custom/GroundWithTopBottomEdges"
{
    Properties
    {
        _GroundColor("Ground Color", Color) = (0.4, 0.2, 0, 1)     // Nâu đất
        _EdgeColor("Edge Color", Color) = (1, 1, 0.5, 1)           // Vàng nhạt
        _EdgeWidth("Edge Width", Float) = 0.2                      // Tỉ lệ chiều cao
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

            float4 _GroundColor;
            float4 _EdgeColor;
            float _EdgeWidth;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Blend theo cạnh trên và dưới
                float bottomBlend = smoothstep(0.0, _EdgeWidth, i.uv.y);
                float topBlend = smoothstep(1.0, 1.0 - _EdgeWidth, i.uv.y);
                float blend = min(bottomBlend, topBlend); // blend càng nhỏ thì càng là viền

                float4 color = lerp(_EdgeColor, _GroundColor, blend);
                return color;
            }
            ENDCG
        }
    }
}

Shader "Custom/WaveShader"
{
	//Unityから渡したい値の宣言
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_WaveSize("WaveSize",float) = 0.0
		_WaveHeight("WaveHeight",float) = 1.0
    }
    SubShader
    {
		Tags{ "RenderType" = "Opaque" }
		LOD 100

        // カリングや深度なし
        Cull Off ZWrite Off ZTest Always

		//HLSLでいうtechnique（RenderState）
		//directX9の固定機能ようなもの
        Pass
        {
			//ここからシェーダを書く
            CGPROGRAM

			//頂点・ピクセル・ジオメトリシェーダであると認識させる
            #pragma vertex vert
            #pragma fragment frag
			#pragma geometry geom

            #include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 NSVertex : TEXCOORD1;
				float3 normal : NORMAL;
            };

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Timer;
			float _PWidth;
			float _WaveSize;
			float _WaveHeight;

			float4x4 Matrix;

			//頂点シェーダの処理
            appdata vert (appdata v)
            {
                return v;
            }

			//ジオメトリシェーダの処理
			[maxvertexcount(3)]
			void geom(triangle appdata input[3], inout TriangleStream<v2f> outStream)
			{
				float Tmp = 0.0;
				[unroll]
				for (int i = 0; i < 3; i++)
				{
					// 頂点シェーダからもらった3頂点それぞれを射影変換して通常のレンダリングと同様にポリゴン位置を決める
					appdata v = input[i];
					v2f o;

					o.vertex = v.vertex;
					o.NSVertex = v.vertex;
					o.normal = v.normal;

					o.uv = TRANSFORM_TEX(v.uv, _MainTex);

					Tmp = sin((o.NSVertex.x - _Timer) / _WaveSize);

					Tmp = (Tmp * 0.5) + 1.0;

					o.vertex.xyz += o.normal.xyz * Tmp * _WaveHeight;
					o.vertex = UnityObjectToClipPos(o.vertex);

					outStream.Append(o);
				}

				outStream.RestartStrip();
			}
            

			//ピクセルシェーダの処理
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}

//旗の処理

//v2f o;
//v.vertex.y += cos((v.vertex.x + _Time.y * _Speed) * _Frequency) * _Amplitude * (v.vertex.x - 5);
//o.pos = UnityObjectToClipPos(v.vertex);

//float amp = 0.5*sin(_Time * 100 + v.vertex.x * 100);
//v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp, v.vertex.z);
////v.normal = normalize(float3(v.normal.x+offset_, v.normal.y, v.normal.z));
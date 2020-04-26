Shader "Custom/WaveShader"
{
	//Unityから渡したい値の宣言
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // カリングや深度なし
        Cull Off ZWrite Off ZTest Always

		//HLSLでいうtechnique（RenderState）
		//directX9の固定機能ようなもの
        Pass
        {
			//ここからシェーダを書く
            CGPROGRAM

			//頂点・ピクセルシェーダであると認識させる
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

			//頂点シェーダの処理
            v2f vert (appdata v)
            {
				v2f o;
				o.vertex.y += cos((v.vertex.x + _Time.y * 1) * 1) * 1 * (v.vertex.x - 5);
                
                o.vertex = UnityObjectToClipPos(o.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

			//ピクセルシェーダの処理
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                //色反転
                col.rgb = col.rgb;
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
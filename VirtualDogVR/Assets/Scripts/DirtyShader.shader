
Shader "Custom/DirtyShader"
{
	Properties
	{
		
		_TexMain("_TexMain", 2D) = "White" {}
		_DirtyTex("_DirtyTex", 2D) = "White" {}
		_DirtyPct("_DirtyPct", Range(0,1)) = 0.0
		
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert //informacion de los vertices. Por cada uno de los vertices se llama a esa funcion. Se manda una estructura a frag. 
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata //informacion de cada uno de los vertices
            {
                float4 vertex : POSITION; //dexinim el tipus de vertes (float4) i la semantica (POSITION)
				float3 uv : TEXCOORD0;
				float3 WorldPosition : TEXCOORD1;
				float3 normal : NORMAL;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION; //tornem la posicio del vertex
				float3 uv : TEXCOORD0;
				float3 WorldPosition : TEXCOORD1;
				float3 normal : NORMAL;

            };
			
			
			sampler2D _TexMain;
			sampler2D _DirtyTex;
			float _DirtyPct;
			

             v2f vert (appdata v)
            {
				v2f o;
				o.normal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
				o.vertex=mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0));
				o.WorldPosition = o.vertex.xyz;
				o.vertex=mul(UNITY_MATRIX_V, o.vertex);
				o.vertex=mul(UNITY_MATRIX_P, o.vertex);
				o.uv=v.uv;
				return o;
            }

             fixed4 frag (v2f i) : SV_Target //Pixel Shader - S'executa per cada pixel.  Entra lo que sale del Pixel Shder. Sale el color (float4(1,0,0,1))
             {
				float4 l_Color = tex2D(_TexMain,i.uv);
				float4 l_Dirty = tex2D(_DirtyTex, i.uv);
				return (l_Color * 1- _DirtyPct) + (l_Dirty * _DirtyPct);
             }
             ENDCG
         }
    }
}

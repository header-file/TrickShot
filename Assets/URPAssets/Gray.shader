Shader "Hidden/Gray"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Grayscale("Grayscale", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _MainTex;
			float _Grayscale;

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 currentText = tex2D(_MainTex, i.uv);

				float grayscale = 0.299 * currentText.r + 0.587 * currentText.g + 0.114 * currentText.b;

				fixed4 color = lerp(currentText, grayscale, _Grayscale);

				currentText.rgb = color;

				return currentText;
			}

            ENDCG
        }
    }
}

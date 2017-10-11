﻿
Shader "Custom/FogofWarShader" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _FogRadius ("FogRadius", Float) = 1.0
    _FogMaxRadius("FogMaxRadius", Float) = 0.5
    _Player1_Pos ("_Player1_Pos", Vector) = (0,0,0,1)
    _Player2_Pos ("_Player2_Pos", Vector) = (0,0,0,1)
    _Player3_Pos ("_Player3_Pos", Vector) = (0,0,0,1)
    _Player4_Pos ("_Player4_Pos", Vector) = (0,0,0,1)
    _Player5_Pos ("_Player5_Pos", Vector) = (0,0,0,1)
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 200
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Off

    CGPROGRAM
    #pragma surface surf Lambert vertex:vert

    sampler2D _MainTex;
    fixed4 _Color;
    float _FogRadius;
    float _FogMaxRadius;
    float4 _Player1_Pos;
    float4 _Player2_Pos;
    float4 _Player3_Pos;
    float4 _Player4_Pos;
    float4 _Player5_Pos;

    struct Input {
        float2 uv_MainTex;
        float2 location;
    };

    float powerForPos(float4 pos, float2 nearVertex);

    void vert(inout appdata_full vertexData, out Input outData) {
        float4 pos = mul(UNITY_MATRIX_MVP, vertexData.vertex);
        float4 posWorld = mul(_Object2World, vertexData.vertex);
        outData.uv_MainTex = vertexData.texcoord;
        outData.location = posWorld.xz;
    }

    void surf (Input IN, inout SurfaceOutput o) {
        fixed4 baseColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;

        float alpha = (1.0 - (powerForPos(_Player1_Pos, IN.location) + powerForPos(_Player2_Pos, IN.location) + powerForPos(_Player3_Pos, IN.location) + powerForPos(_Player4_Pos, IN.location) + powerForPos(_Player5_Pos, IN.location)));

        o.Albedo = baseColor.rgb;
        o.Alpha = alpha;
    }

    //return 0 if (pos - nearVertex) > _FogRadius
    float powerForPos(float4 pos, float2 nearVertex) {
        float atten = clamp(_FogRadius - length(pos.xz - nearVertex.xy), 0.0, _FogRadius);

        return (1.0/_FogMaxRadius)*atten/_FogRadius;
    }

    ENDCG
}

Fallback "Transparent/VertexLit"
}
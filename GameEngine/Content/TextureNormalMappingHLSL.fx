bool TextureEnabled = false;

texture Texture;

float3 DiffuseColor = float3(1, 1, 1);
float3 AmbientColor = float3(0.3f, 0.3f, 0.3f);

float3 LightDirection = float3(1, 1, 1);
float3 LightColor = float3(1, 1, 1);

sampler TextureSampler = sampler_state
{
    Texture = <Texture>;
};


float4x4 World;
float4x4 View;
float4x4 Projection;

struct VertexShaderInput
{
    float4 Position : SV_Position0;
    float2 UV : TEXCOORD0;
    float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : SV_Position0;
    float2 UV : TEXCOORD0;
    float3 Normal : TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    
    output.Position = mul(viewPosition, Projection);
    output.UV = input.UV;
    output.Normal = normalize(mul(input.Normal, World));

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float3 color = DiffuseColor;

    if (TextureEnabled)
    {
        color *= tex2D(TextureSampler, input.UV).rgb;
    }

    float3 normal = normalize(input.Normal);
    float3 lightDir = normalize(LightDirection);

    float lambert = saturate(dot(-lightDir, normal));
    float3 lighting = AmbientColor + (lambert * LightColor);

    float3 finalColor = saturate(color * lighting);

    return float4(finalColor, 1);
}

technique BasicTechnique
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}


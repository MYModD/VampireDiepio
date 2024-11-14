float3 hash3(float3 p)
{
    float3 q = float3(
        dot(p, float3(127.1, 311.7, 74.7)),
        dot(p, float3(269.5, 183.3, 246.1)),
        dot(p, float3(113.5, 271.9, 124.6))
    );
    return frac(sin(q) * 43758.5453123);
}

float voronoi(float3 p, float time)
{
    float3 i = floor(p);
    float3 f = frac(p);
    
    float minDist = 1.0;
    
    for (int z = -1; z <= 1; z++)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                float3 cell = float3(float(x), float(y), float(z));
                float3 offset = hash3(i + cell);
                offset = 0.5 + 0.5 * sin(time + 6.2831 * offset);
                
                float3 r = cell + offset - f;
                float d = length(r);
                minDist = min(minDist, d);
            }
        }
    }
    
    return minDist;
}

float fbm(float3 p, float time)
{
    float value = 0.0;
    float amplitude = 0.5;
    float frequency = 1.0;
    
    for (int i = 0; i < 4; i++)
    {
        float3 q = p * frequency;
        q.x += time * 0.1;
        q.y += sin(time * 0.05) * 2.0;
        value += amplitude * voronoi(q, time);
        frequency *= 2.0;
        amplitude *= 0.5;
    }
    
    return value;
}

void Neural_float(float3 Position, float Time, out float3 Out)
{
    float3 p = Position * 3.0;
    
    // ニューロンパターン
    float neural = fbm(p, Time);
    
    // シナプス接続パターン
    float synapses = fbm(p + float3(1.0, 2.0, 3.0), Time);
    
    // 活性化パターン
    float activation = fbm(p * 0.5, Time);
    
    // カラー計算
    float3 baseColor = float3(0.1, 0.3, 0.8);
    float3 activeColor = float3(0.8, 0.2, 0.1);
    float3 finalColor = lerp(baseColor, activeColor, neural * synapses);
    
    finalColor *= (1.0 + activation * 0.5);
    Out = finalColor;
}
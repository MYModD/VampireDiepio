// CustomFunctionノードに入れるコード
void ComplexNetworkNoise_float(float3 Position, float Time, out float3 Out)
{
    // 基本的なハッシュ関数
    float3 hash(float3 p)
    {
        p = float3(
            dot(p, float3(127.1, 311.7, 74.7)),
            dot(p, float3(269.5, 183.3, 246.1)),
            dot(p, float3(113.5, 271.9, 124.6))
        );
        return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
    }
    
    // 3次元パーリンノイズ
    float perlin(float3 p)
    {
        float3 pi = floor(p);
        float3 pf = frac(p);
        float3 pf2 = pf * pf * (3.0 - 2.0 * pf);
        
        return lerp(
            lerp(
                lerp(dot(hash(pi + float3(0,0,0)), pf - float3(0,0,0)),
                     dot(hash(pi + float3(1,0,0)), pf - float3(1,0,0)), pf2.x),
                lerp(dot(hash(pi + float3(0,1,0)), pf - float3(0,1,0)),
                     dot(hash(pi + float3(1,1,0)), pf - float3(1,1,0)), pf2.x), pf2.y),
            lerp(
                lerp(dot(hash(pi + float3(0,0,1)), pf - float3(0,0,1)),
                     dot(hash(pi + float3(1,0,1)), pf - float3(1,0,1)), pf2.x),
                lerp(dot(hash(pi + float3(0,1,1)), pf - float3(0,1,1)),
                     dot(hash(pi + float3(1,1,1)), pf - float3(1,1,1)), pf2.x), pf2.y), pf2.z) * 0.5 + 0.5;
    }
    
    // フラクタルノイズ生成
    float fbm(float3 p)
    {
        float result = 0.0;
        float amplitude = 0.5;
        float frequency = 1.0;
        float3 shift = float3(100, 100, 100);
        
        for(int i = 0; i < 6; i++)
        {
            float3 q = p * frequency + shift;
            result += amplitude * perlin(q);
            
            // 回転行列による変形
            float3x3 rot = float3x3(
                cos(Time), sin(Time), 0,
                -sin(Time), cos(Time), 0,
                0, 0, 1
            );
            shift = mul(rot, shift);
            
            frequency *= 2.0;
            amplitude *= 0.5;
        }
        return result;
    }
    
    // ワームのような動きを生成
    float worms(float3 p)
    {
        float3 q = p;
        q.xy += float2(sin(Time + q.z * 2.0), cos(Time + q.z * 1.5)) * 0.5;
        
        float result = fbm(q);
        result += fbm(q * 2.0 + Time * 0.1) * 0.5;
        result += fbm(q * 4.0 - Time * 0.2) * 0.25;
        
        return result;
    }
    
    // ネットワークパターン生成
    float network(float3 p)
    {
        float result = 0.0;
        float3 cellId = floor(p);
        float3 localPos = frac(p);
        
        // 近傍セルをチェック
        for(int z = -1; z <= 1; z++)
        {
            for(int y = -1; y <= 1; y++)
            {
                for(int x = -1; x <= 1; x++)
                {
                    float3 offset = float3(x, y, z);
                    float3 h = hash(cellId + offset);
                    float3 nodePos = offset + h * 0.5 + 0.5;
                    
                    // ノードからの距離
                    float dist = length(localPos - nodePos);
                    
                    // 時間によってノードの影響力を変化
                    float strength = sin(Time + dot(h, float3(1,1,1))) * 0.5 + 0.5;
                    result += smoothstep(0.3, 0.0, dist) * strength;
                }
            }
        }
        
        return result;
    }
    
    // 最終的なパターンの合成
    float3 finalPattern = float3(0,0,0);
    
    // ベースとなるワームパターン
    float wormPattern = worms(Position * 2.0);
    
    // ネットワークパターン
    float networkPattern = network(Position * 3.0);
    
    // パターンの合成
    finalPattern.r = wormPattern;
    finalPattern.g = networkPattern;
    finalPattern.b = fbm(Position * 4.0 + float3(networkPattern, wormPattern, 0));
    
    Out = finalPattern;
}

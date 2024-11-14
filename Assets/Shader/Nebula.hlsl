float3 hash3(float3 p)
{
    float3 q = float3(
        dot(p, float3(127.1, 311.7, 74.7)),
        dot(p, float3(269.5, 183.3, 246.1)),
        dot(p, float3(113.5, 271.9, 124.6))
    );
    return frac(sin(q) * 43758.5453123);
}

float2 voronoi(float3 p, float time, float cellSize)
{
    float3 n = floor(p * cellSize);
    float3 f = frac(p * cellSize);
    
    float2 closest = float2(2.0, 2.0);
    float3 mid_point = float3(0, 0, 0);
    
    // 周囲のセルをチェック
    for (int z = -1; z <= 1; z++)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                float3 offset = float3(x, y, z);
                float3 cell = n + offset;
                
                // セル内のランダムな点を生成
                float3 points = hash3(cell);
                
                // アニメーション
                points = 0.5 + 0.5 * sin(time * float3(0.5, 0.7, 0.9) + 6.2831 * points);
                
                float3 diff = offset + points
                -f;
                float dist = length(diff);
                
                if (dist < closest.x)
                {
                    closest.y = closest.x;
                    closest.x = dist;
                    mid_point = cell + points;
                }
                else if (dist < closest.y)
                {
                    closest.y = dist;
                }
            }
        }
    }
    
    // 最も近い点との距離と、2番目に近い点との距離の差を返す
    return closest;
}

float smooth_voronoi(float3 p, float time, float cellSize, float smoothness)
{
    float2 v = voronoi(p, time, cellSize);
    
    // セル間の遷移をスムーズに
    float cells = 1.0 - smoothstep(0.0, smoothness, v.y - v.x);
    
    return cells;
}

float organic_pattern(float3 p, float time, float cellSize, float smoothness, float layerStrength)
{
    float pattern = 0.0;
    float strength = 1.0;
    float scale = 1.0;
    
    // 複数レイヤーを重ね合わせ
    for (int i = 0; i < 3; i++)
    {
        pattern += smooth_voronoi(p * scale, time, cellSize, smoothness) * strength * layerStrength;
        strength *= 0.5;
        scale *= 2.0;
    }
    
    return pattern;
}

void OrganicVoronoi_float(
    float3 Position,
    float Time,
    float Scale, // 全体的なスケール (推奨: 0.1-5.0)
    float CellSize, // セルの大きさ (推奨: 1.0-10.0)
    float Smoothness, // セル間の滑らかさ (推奨: 0.1-0.5)
    float Speed, // アニメーション速度 (推奨: 0.1-2.0)
    float LayerStrength, // レイヤーの強さ (推奨: 0.5-2.0)
    float Contrast, // コントラスト (推奨: 0.5-3.0)
    out float3 Out)
{
    float3 scaledPos = Position * Scale;
    float animTime = Time * Speed;
    
    // メインパターンの生成
    float pattern = organic_pattern(
        scaledPos,
        animTime,
        CellSize,
        Smoothness,
        LayerStrength
    );
    
    // コントラスト調整
    pattern = pow(pattern, Contrast);
    
    // 出力
    Out = pattern.xxx;
}